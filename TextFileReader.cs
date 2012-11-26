using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

public static class TextFileReader
{
    public static string Read(string fileName) 
    {
        try
        {
            using (Stream stream = Assembly.GetExecutingAssembly()
                                   .GetManifestResourceStream(fileName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}

