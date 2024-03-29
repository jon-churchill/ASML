﻿using ASMLEngineSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asml_Level100Pikachus.Factories;

namespace Asml_Level100Pikachus
{
    /// <summary>
    /// Client
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Runs the missile launcher 
        /// </summary>
        public void Fire()
        {
            Launcher.Fire();
        }

        public int numBullets;

        public void MoveBy(double phi, double theta)
        {
            Launcher.MoveBy(phi, theta);
        }

        public void SetNum(int num)
        {
            numBullets = num;
        }

        public int GetNum()
        {
            return numBullets;
        }

        public void MoveTo(double phi, double theta)
        {
            Launcher.MoveTo(phi, theta);
        }


        public void LoadTargets(string filePath)
        {
            Factory factory = Factory.GetInstance();
            FileReader reader = factory.CreateReader(filePath);
            List<Target> targets = reader.ReadLines(filePath);

            TargetManager tm = TargetManager.GetInstance();
            tm.ClearTargets();
            tm.AddTargets(targets);
        }

        /// <summary>
        /// Reference to a missile launcher.
        /// </summary>
        public IMissileLauncher Launcher
        {
            get;
            set;
        }

        private static Controller controllerInstance;

        private Controller()
        {

        }

        public static Controller GetInstance()
        {
            if (controllerInstance == null)
            {
                controllerInstance = new Controller();
            }            
            return controllerInstance;            
        }
    }
}
