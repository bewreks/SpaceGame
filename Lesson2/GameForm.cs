﻿using System;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Lesson2.Events;
using Lesson2.Loggers;
using Lesson2.Scenes;
using Timer = System.Windows.Forms.Timer;

namespace Lesson2
{
    public class GameForm : Form
    {
        private Thread _updateThread;
        private Thread _drawThread;

        private Thread _gameThread;

        private Player _player;

        public GameForm()
        {
//            Logger.Init(new ConsoleLogger());
//            Logger.AddLogger(new FileLogger());

            Logger.Error("test");
            Logger.Error("test {0}", 1);

            Logger.Print("Start form");
            // Настройка формы
            Width = 800;
            Height = 600;

            var size = new Size(Width, Height);
            MinimumSize = size;
            MaximumSize = size;

            MaximizeBox = false;
            Logger.Print("Form created");

            // Запуск потоков на Update + отрисовку
            _updateThread = new Thread(() =>
            {
                while (true)
                {
                    Drawer.Draw();
                }
            });
            _drawThread = new Thread(() =>
            {
                while (true)
                {
                    Drawer.Draw();
                }
            });

            _gameThread = new Thread(Game);
            _gameThread.IsBackground = true;

            _player = new Player();

            Shown += Start;
            Closed += End;
            KeyDown += OnKeyDown;
            MouseMove += OnMouseMove;
            MouseDown += OnMouseClick;

            // Инициализация класса отрисовки
            // В целом бесполезен, но так красивее и правильнее
            Drawer.Init(this);
        }

        private void Game()
        {
            while (true)
            {
                Drawer.Update();
                Drawer.Draw();
            }
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            EventManager.DispatchEvent(EventManager.Events.ShootEvent);
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            EventManager.DispatchEvent(EventManager.Events.MoveEvent, new MouseMoveGameEvent(e.X, e.Y));
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    EventManager.DispatchEvent(EventManager.Events.UpEvent);
                    break;
                case Keys.Down:
                    EventManager.DispatchEvent(EventManager.Events.DownEvent);
                    break;
                case Keys.ControlKey:
                    EventManager.DispatchEvent(EventManager.Events.ShootEvent);
                    break;
            }
        }

        private void End(object sender, EventArgs e)
        {
            Logger.Print("Form closed");
//            _drawThread.Suspend();
//            _updateThread.Suspend();
            _gameThread.Suspend();
        }

        private void Start(object sender, EventArgs e)
        {
            Logger.Print("Form shown");
            Drawer.SetScene(new SplashScene());
//            Drawer.SetScene(new SpaceScene());
//            _updateThread.Start();
//            _drawThread.Start();

            _gameThread.Start();
        }
    }

    public class Player
    {
        private int _hp;
        private int _score;

        public int Hp => _hp;
        public int Score => _score;

        public Player()
        {
            _hp = 100;
            _score = 0;
            
            EventManager.AddEventListener(EventManager.Events.ChangeScoreEvent, OnChangeScore);
            EventManager.AddEventListener(EventManager.Events.ChangeEnergyEvent, OnChangeEvent);
        }

        private void OnChangeEvent(IEventArgs args)
        {
            _hp += (args as ChangeScoreEvent).Score;
            Logger.Print("HP: {0}", Hp);
        }

        private void OnChangeScore(IEventArgs args)
        {
            _score += (args as ChangeScoreEvent).Score;
            Logger.Print("Score: {0}", Score);
        }
    }
}