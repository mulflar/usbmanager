namespace usbmanager
{
    using System;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using static DeviceManager;

    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length==1)
            {
                switch (args[0])
                {
                    case "revisar":
                        RevisarFichero();
                        break;
                    case "montar":
                        Montar();
                        break;
                    case "desmontar":
                        Desmontar();
                        break;
                }
            }
        }

        static void Desmontar()
        {
            var config = ReadXML();
            Guid mouseGuid = new Guid(config.GUID);
            string instancePath = @config.PATH.Replace("&amp;", "&");
            DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, false);
        }

        static void Montar()
        {
            var config = ReadXML();
            Guid mouseGuid = new Guid(config.GUID);
            string instancePath = @config.PATH.Replace("&amp;","&");
            DeviceHelper.SetDeviceEnabled(mouseGuid, instancePath, true);
        }
        static void RevisarFichero()
        {
            string text = File.ReadAllText("configuracion.xml");
            text = text.Replace("&", "&amp;");
            File.WriteAllText("configuracion.xml", text);
        }


        static configuracion ReadXML()
        {
            string rutaConfig = AppDomain.CurrentDomain.BaseDirectory + "configuracion.xml";
            if (File.Exists(rutaConfig))
            {
                XmlSerializer _s = new XmlSerializer(typeof(configuracion));
                var tempConf = (configuracion)_s.Deserialize(new XmlTextReader(rutaConfig));
                return tempConf;
            }
            else
            {
                Console.WriteLine("FICHERO DE CONFIGURACION INEXISTENTE, Pulse cualquier tecla para salir");
                Console.ReadKey();
                Environment.Exit(0);
                return null;
            }
        }
    }
}
