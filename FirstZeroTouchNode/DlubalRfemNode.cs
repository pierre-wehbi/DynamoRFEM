using System;
using System.IO;
using Dlubal.WS.Rfem6.Application;
using ApplicationClient = Dlubal.WS.Rfem6.Application.RfemApplicationClient;
using Dlubal.WS.Rfem6.Model;
using ModelClient = Dlubal.WS.Rfem6.Model.RfemModelClient;

using NLog;
using System.ServiceModel;
using System.Linq;

using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;



namespace FirstZeroTouchNode
{
    public class DlubalRfemNode
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static string CreateCompleteModel(string modelName, List<string> materialNames)
        {
            //            Logger logger = LogManager.GetCurrentClassLogger();
            EndpointAddress address = new EndpointAddress("http://localhost:8081");
            BasicHttpBinding binding = new BasicHttpBinding
            {
                SendTimeout = new TimeSpan(0, 0, 180),
                UseDefaultWebProxy = true,
            };

            ApplicationClient application = null;

            try
            {
                // Connect to RFEM6 application
                application = new ApplicationClient(binding, address);
                application_information ApplicationInfo = application.get_information();
                logger.Info($"Connected to {ApplicationInfo.name} Version: {ApplicationInfo.version}");

                // Create new model
                string modelUrl = application.new_model(modelName);
                ModelClient model = new ModelClient(binding, new EndpointAddress(modelUrl));
                model.reset();

                // Define materials, sections, nodes, etc.
                DefineModelComponents(model, materialNames);

                // Finish model setup
                //                model.finish_modification();

                return $"Model {modelName} created and set up successfully.";
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"Error in CreateCompleteModel: {ex.Message}");
                return $"Error: {ex.Message}";
            }
            finally
            {
                // Close application connection, but leave the model open
                if (application != null)
                {
                    // application.close_model(0, false); // Commented out to keep the model open
                    // Optional: Add a short delay
                    System.Threading.Thread.Sleep(3000); // Wait for 1 second
                    application.Close(); // Close application connection
                }
            }
        }

        private static void DefineModelComponents(ModelClient model, List<string> materialNames)
        {
            try
            {
                model.begin_modification("Define Components");

                int materialNo = 1;
                foreach (var materialName in materialNames)
                {
                    material newMaterial = new material
                    {
                        no = materialNo++,
                        name = materialName
                        // Optionally, set a default material type here if required
                    };

                    model.set_material(newMaterial);
                }

                model.finish_modification(); // Commit the changes
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error in DefineModelComponents: " + ex.Message);
                model.cancel_modification(); // Cancel if there's an error
            }
        }
    }
}