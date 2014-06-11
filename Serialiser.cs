using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine {
    public class Serialiser {
        public Scene LoadScene(String sceneName) {
            StreamReader reader = new StreamReader(Environment.CurrentDirectory + "/game/scenes/" + sceneName + ".scene");
            String jsonScene = reader.ReadToEnd();
            Scene scene = JsonConvert.DeserializeObject<Scene>(jsonScene);
            reader.Close();
            return scene;
        }

        public void SaveScene(Scene scene) {
            String jsonScene = JsonConvert.SerializeObject(scene, Formatting.Indented);
            Console.Out.WriteLine(jsonScene);
            StreamWriter file = new StreamWriter(Environment.CurrentDirectory + "/game/scenes/" + scene.Name + ".scene");
            file.Write(jsonScene);
            file.Close();
        }
    }
}
