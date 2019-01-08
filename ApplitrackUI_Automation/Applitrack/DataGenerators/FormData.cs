using System;

namespace ApplitrackUITests.DataGenerators
{
    public class FormData 
    {
        public string FormType = "Standard Form";
        public string FormCategory = "Absence Forms";

        private static Random _rand = new Random();
        public string FormTitle = "Auto Test Form " + _rand.Next();

        /*
        private string _formTitle;
        public string FormTitle
        {
            get { return this._formTitle; }
            set
            {
                _formTitle = value;
                Random generator = new Random();
                this._formTitle = "Auto Test Form" + generator;
            }
        }
         */
    }
}
