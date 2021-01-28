using System;
using System.IO;
using System.Reflection;

namespace Corruption
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            string path = System.IO.Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            if (!File.Exists(path + "/input"))
            {
                Directory.CreateDirectory(path + "/input");
            }
            
            if (!File.Exists(path + "/output"))
            {
                Directory.CreateDirectory(path + "/output");
            }
            
            
            
            foreach (var file in Directory.GetFiles(path + "/input"))
            {
                bool isDoc = false;
                
                
                string output = System.IO.Directory.GetCurrentDirectory() + "/output/" +  Path.GetFileName(file);

                if (output.EndsWith("docx"))
                {
                    isDoc = true;
                }
                if (!File.Exists(output ))
                {
                    File.Exists(output );
                }
                
                Console.WriteLine(file);
                using (StreamReader sr = new StreamReader(file)) 
                {
                    while (true)
                    {
                        var line = sr.ReadLine();
                        
                        if (line == null)
                        {
                            break;
                        }

                      

                            using (StreamWriter sw = new StreamWriter(output))
                            {
                                
                                if (isDoc)
                                {
                                    String encrypt = line;
                                    if (line.Length > 100)
                                    {

                                        encrypt = StringCipher.shorten(encrypt, 30);

                                    }
                                    else
                                    {
                                        if (encrypt.Contains("<"))
                                        {
                                            
                                            if(random.Next(100) < 25)
                                            {
                                                encrypt = encrypt.Replace("<", "");
                                            }
                                            
                                        }
                                    }

                                    

                                    
                                    sw.WriteLine(encrypt);
                                }
                                else
                                {
                                String encrypt = StringCipher.FunForEncrypt(line);
                                sw.WriteLine(encrypt);
                                
                            }
                        }

                    }
                }
            }
        }
    }
}
