using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteBoxEngine.Abstracts;
using InfiniteBoxEngine.Object;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfiniteBoxEngine {
    public abstract class Scene : Nameable {
        private List<String> sceneContent = new List<string>();
        private List<GameObject> gameObjects = new List<GameObject>();

        public Scene(String name) {
            this.Name = name;
        }

        [JsonProperty(Order = 1)]
        public List<String> SceneContent {
            get { return sceneContent; }
            set { sceneContent = value; }
        }

        [JsonProperty(Order = 2)]
        public List<GameObject> GameObjects {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        public void CreateNullBodys(World world) {
            foreach (GameObject obj in gameObjects) {
                obj.CreateBody(world);
                obj.Body.BodyType = obj.Type;
                obj.Body.CollisionCategories = Category.All;
            }
        }

        public void UpdateNullTextures() {
            foreach (GameObject gameObject in gameObjects) {
                if (gameObject.Texture == null && gameObject.TextureName != null) {
                    gameObject.Texture = EngineContentManager.GetTexture(gameObject.TextureName);
                }
            }
        }

        public void RegisterNewTexture(String name) {
            this.sceneContent.Add(name);
            EngineContentManager.LoadSceneTexture(this, name);
        }

        public abstract void Draw();

        public abstract void DrawForeground();

        public abstract void LoadContent();

        public abstract void Update(GameTime gameTime);

        public abstract void HandleInput(ControlManager controlManager);
    }
}
