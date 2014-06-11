using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using InfiniteBoxEngine.Abstracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;

namespace InfiniteBoxEngine.Object
{
    public class GameObject : Nameable
    {
        Texture2D texture;
        String textureName;
        Vector2 position, position2;
        float width, height;
        Body body;
        bool drawable = true;
        BodyType type;
        BodyShape shape;

        /// <summary>
        /// Creates a rectangular textured game object 
        /// </summary>
        /// <param name="world">World the object belongs to</param>
        /// <param name="name">Name of the object</param>
        /// <param name="texture">Objects texture</param>
        /// <param name="position">Initial position in the world (pixels)</param>
        /// <param name="width">Width of the objects collision box</param>
        /// <param name="height">Hieght of the objects collision box</param>
        public GameObject(World world, String name, String textureName, Vector2 position, float width, float height, BodyType type, bool drawable)
        {
            this.Name = name;
            if(textureName != null)
                this.texture = EngineContentManager.GetTexture(textureName);
            this.textureName = textureName;
            this.position = position;
            this.width = width;
            this.height = height;
            this.type = type;
            this.drawable = drawable;
            this.shape = BodyShape.Rectangle;
            if (world != null)
            {
                body = BodyFactory.CreateRectangle(world, width, height, 1f, position, name);
                this.body.BodyType = type;
                body.CollisionCategories = Category.All;
            }
            else
                body = null;
        }

        /// <summary>
        /// Creates a collidable edge with no texture
        /// </summary>
        /// <param name="world">World the object belongs to</param>
        /// <param name="name">Name of the object</param>
        /// <param name="start">Start point of the edge</param>
        /// <param name="end">End point of the edge</param>
        public GameObject(World world, String name, Vector2 start, Vector2 end)
        {
            this.Name = name;
            this.position = start;
            this.position2 = end;
            this.shape = BodyShape.Edge;
            body = BodyFactory.CreateEdge(world, start, end, name);
        }

        /// <summary>
        /// Creates gameObjects - Json.NET use only - 
        /// </summary>
        /// <param name="world">World the object belongs to</param>
        /// <param name="name">Name of the object</param>
        /// <param name="texture">Objects texture</param>
        /// <param name="position">Initial position in the world (pixels)</param>
        /// <param name="width">Width of the objects collision box</param>
        /// <param name="height">Hieght of the objects collision box</param>
        [JsonConstructor]
        public GameObject(World world, String name, String textureName, Vector2 position, Vector2 position2, float width, float height, BodyType type, bool drawable, BodyShape shape)
        {
            this.Name = name;
            if (textureName != null)
                this.texture = EngineContentManager.GetTexture(textureName);
            this.textureName = textureName;
            this.position = position;
            this.position2 = position2;
            this.width = width;
            this.height = height;
            this.type = type;
            this.drawable = drawable;
            this.shape = shape;

            if (world != null)
            {
                CreateBody(world);
                this.body.BodyType = type;
                body.CollisionCategories = Category.All;
            }
            else
                body = null;
        }

        public void CreateBody(World world){
            switch(shape){
                case BodyShape.Rectangle :
                    this.Body = BodyFactory.CreateRectangle(world, width, height, 1f, position, this.Name);
                    break;
                case BodyShape.Edge :
                    this.Body = BodyFactory.CreateEdge(world, position, position2, this.Name);
                    break;
            }
        }

        /// <summary>
        /// Draws the game object using stored texture
        /// </summary>
        /// <param name="sb">Spritebatch used for drawing</param>
        /// <param name="gt">Gametime used for animation</param>
        public void Draw(Camera2D camera, SpriteBatch sb, GameTime gt)
        {
            if(drawable)
                sb.Draw(texture, FlipY(body.Position), null, Color.White, body.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
                //sb.Draw(texture, body.Position, null, Color.White, body.Rotation, new Vector2(texture.Width / 2, texture.Height / 2), 1f, SpriteEffects.None, 0f);
        }

        public bool Drawable
        {
            get { return drawable; }
            set { drawable = value; }
        }

        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public Vector2 Position
        {
            get { position = body.Position;  return position; }
            set { position = value; body.Position = value; }
        }

        public Vector2 Position2
        {
            get { return position2; }
            set { position2 = value; }
        }

        public String TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }

        public BodyType Type
        {
            get { return type; }
            set
            {
                type = value;
                Body.BodyType = value;
            }
        }

        public BodyShape Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        [JsonIgnore]
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        [JsonIgnore]
        public Body Body
        {
            get { return body; }
            set { body = value; }
        }

        public Vector2 FlipY(Vector2 vector) { return new Vector2(vector.X, -vector.Y); }
    }
}
