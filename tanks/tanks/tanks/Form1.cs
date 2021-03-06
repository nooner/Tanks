﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace tanks
{
    public partial class Controller_MainForm : Form
    {
        View view;
        Model model;
        Thread modelPlay;

        public Controller_MainForm() : this(10) { }
        public Controller_MainForm(int amountTanks) : this(amountTanks, 20) { }
        public Controller_MainForm(int amountTanks, int speedGame) : this(amountTanks, speedGame, 25) { }

        public Controller_MainForm(int amountTanks, int speedGame, int amountWalls)
        {
            InitializeComponent();
            model = new Model(amountTanks, speedGame, amountWalls);
            view = new View(model);
            view.Location = new Point(50, 50);
            this.Controls.Add(view);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (model.gameStatus == GameStatus.PLAY)
            {
                modelPlay.Abort();
                model.gameStatus = GameStatus.STOP;
            }
            else
            {
                model.gameStatus = GameStatus.PLAY;
                modelPlay = new Thread(model.Play);
                modelPlay.Start();

                view.Invalidate();
            }
        }

        private void Controller_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (modelPlay != null)
            {
                model.gameStatus = GameStatus.STOP;
                modelPlay.Abort();
            }

            if (DialogResult.Cancel != MessageBox.Show("Вы точно хотите выйти?", "Танки", MessageBoxButtons.OKCancel))
                e.Cancel = false;
            else
                e.Cancel = true;

            
            
            
        }
    }
}

