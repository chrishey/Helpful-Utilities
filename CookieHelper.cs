public class CookieHelper
	{
		protected virtual HttpCookieCollection ResponseCookies { get { return HttpContext.Current.Response.Cookies; } }
        
        protected virtual HttpCookieCollection RequestCookies { get { return HttpContext.Current.Request.Cookies; } }

        public virtual void Insert(string key, object value, TimeSpan slidingExpiration) { Insert(key, value, DateTime.Now.Add(slidingExpiration)); }

        public virtual void Insert(string key, string valueKey, object value, TimeSpan slidingExpiration) { Insert(key, valueKey, value, DateTime.Now.Add(slidingExpiration)); }

		public virtual void Insert<T>(string key, T value, DateTime absoluteExpiration) where T : class { Insert(key, value, absoluteExpiration); }

		public virtual void Insert<T>(string key, T value, TimeSpan slidingExpiration) where T : class { Insert(key, value, slidingExpiration); }

		public virtual void Insert(string key, object value) {
			var o = Get(key, false);

			if (o == null) {
				var cookie = new HttpCookie(key, value.ToString());
				cookie.HttpOnly = true;
				ResponseCookies.Add(cookie);
			} else {
				o.Value = value.ToString();
				ResponseCookies.Set(o);
			}
		}

		public virtual void Insert(string key, object value, DateTime absoluteExpiration) {
			var cookie = new HttpCookie(key, value.ToString());
			cookie.Expires = absoluteExpiration;
			cookie.HttpOnly = true;
			ResponseCookies.Add(cookie);
		}

		

		public virtual void Insert(string key, string valueKey, object value) {
			var o = Get(key, false);
            
            if (o == null) {
				var cookie = new HttpCookie(key);
				cookie.HttpOnly = true;
				cookie.Values.Set(valueKey, value.ToString());
				ResponseCookies.Add(cookie);
			} else {
				o.Values.Set(valueKey, value.ToString());
				ResponseCookies.Set(o);
			}
		}

		public virtual void Insert(string key, string valueKey, object value, DateTime absoluteExpiration) {
			var o = Get(key, false);

			if (o == null) {
				var cookie = new HttpCookie(key);
				cookie.Values.Set(valueKey, value.ToString());
				cookie.Expires = absoluteExpiration;
				cookie.HttpOnly = true;
				ResponseCookies.Add(cookie);
			} else {
				o.Values.Set(valueKey, value.ToString());
			}
		}

        public virtual void Clear() {
            IEnumerator enumerator = ResponseCookies.GetEnumerator();
            while (enumerator.MoveNext()) {
                ResponseCookies.Remove(enumerator.ToString());
            }
        }

        public virtual HttpCookie Get<T>(string key) where T : class {
            var o = RequestCookies.Get(key);
            if (o == null) return null;
            return o;
        }

		public virtual HttpCookie Get(string key, bool readOnly) {
			if (readOnly) return RequestCookies.Get(key);
            return ResponseCookies.Get(key);
		}

		public virtual void Remove(string key) { ResponseCookies.Remove(key); }

		

		public virtual object this[string key] {
            get { return RequestCookies.Get(key); }
			set {
                var cookie = new HttpCookie(key, value.ToString());
				RequestCookies.Add(cookie);
			}
		}

        public HttpCookie Fetch<T>(string key, T value, TimeSpan slidingExpiration) where T : class { return Fetch(key, value, DateTime.Now.Add(slidingExpiration)); }

		public HttpCookie Fetch<T>(string key, T value, DateTime absoluteExpiration) where T : class {
			var o = RequestCookies.Get(key);
            if (o == null) {
                if (value == null) return null;

                var cookie = new HttpCookie(key, value.ToString());
                cookie.Expires = absoluteExpiration;
                cookie.HttpOnly = true;
                ResponseCookies.Add(cookie);
                return cookie;
            } else { return o; }
		}

        public HttpCookie Fetch<T>(string key, string valueKey, T value, TimeSpan slidingExpiration) where T : class { return Fetch(key, valueKey, value, DateTime.Now.Add(slidingExpiration)); }

		public HttpCookie Fetch<T>(string key, string valueKey, T value, DateTime absoluteExpiration) where T : class {
			var o = RequestCookies.Get(key);
			if (o == null) {
				if (value == null) return null;

				var cookie = new HttpCookie(key);
				cookie.Expires = absoluteExpiration;
				cookie.HttpOnly = true;
				cookie.Values.Add(valueKey, value.ToString());
				ResponseCookies.Add(cookie);
				return cookie;
			} else {
				o.Values.Set(valueKey, value.ToString());
				return o;
			}
		}
    }