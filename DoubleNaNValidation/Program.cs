using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace DoubleNaNValidation
{
    class Program
    {
        public static void Main()
        {
            var xmlSchemaSetDocument = new XmlSchemaSet();
            var variable = System.IO.File.ReadAllText("DoubleNaN.xsd");

            xmlSchemaSetDocument.CompilationSettings.EnableUpaCheck = false;

            using (var strReader = new StringReader(variable))
            {
                using (var myXmlTextReader = new XmlTextReader(strReader))
                {
                    var readerSettings = new XmlReaderSettings
                    {
                        ValidationType = ValidationType.Schema,
                        DtdProcessing = DtdProcessing.Ignore
                    };

                    using (XmlReader myXmlReader = XmlReader.Create(myXmlTextReader, readerSettings))
                    {
                        XmlSchema xmlSchema = XmlSchema.Read(myXmlReader, SchemaValidationHandler);
                        xmlSchemaSetDocument.Add(xmlSchema);
                    }
                }
            }

            xmlSchemaSetDocument.Compile();

            var xmlDocument = System.IO.File.ReadAllText("inputDoubleNaN.xml");           
            
            // set validation settings
            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema; // set validation type as schema validation
            settings.Schemas = xmlSchemaSetDocument;
            settings.ValidationEventHandler += ValidationHandler; // bind validation handler to handle validation error.

            // Create XML reader with using reader settings
            using (XmlReader validatingReader = XmlReader.Create(new StringReader(xmlDocument), settings))
            {
                while (validatingReader.Read()) { }
            }

        }
        private static void ValidationHandler(object sender, ValidationEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void SchemaValidationHandler(object sender, ValidationEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
