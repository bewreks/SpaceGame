﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Lesson2.Loggers;
using Lesson2.Scenes;

namespace Lesson2
{
    public static class Drawer
    {
        private static Scene _scene;

        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;

        private static int _width;
        private static int _height;

        private static DateTime _dateTime;

        public static int Width
        {
            get => _width;
            set
            {
                if (value > 1000 || value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _width = value;
            }
        }

        public static int Height
        {
            get => _height;
            set
            {
                if (value > 1000 || value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _height = value;
            }
        }

        // Метод установки сцены
        // Сразу же нужно создать все объекты сцены
        public static void SetScene(Scene scene)
        {
            _scene = scene;
            _scene.Load();
            _scene.OnShown();
        }

        public static void SetLoadedScene(Scene scene)
        {
            _scene = scene;
            _scene.OnShown();
        }

        public static void Init(Form form)
        {
            var g = form.CreateGraphics();
            _context = BufferedGraphicsManager.Current;
            Width = form.Width;
            Height = form.Height;
            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            _dateTime = DateTime.Now;
        }

        // Очитить, отрисовать сцену, если есть, отрендерить
        public static void Draw()
        {
            try
            {
                //Нужно блокировать Graphics для все объектов
                _buffer.Graphics.Clear(Color.Black);

                _scene?.Draw(_buffer.Graphics);

                _buffer.Render();
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
        }

        public static void Update()
        {
            try
            {
                {
                    var dateTime = DateTime.Now;
                    _scene?.Update((float) (dateTime - _dateTime).TotalSeconds);
                    _dateTime = dateTime;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
        }
    }
}