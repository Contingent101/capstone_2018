using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Visual_Circuit_Builder
{
    class Gate
    {
        private string id;
        private Point point;
        
        private int in1;
        private int in2;
        private int value;
        private int nextGate;
        private int out3;


        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                //fcn
                this.value = value;
            }
        }
        /// <summary>
        /// ID is the id of the gate
        /// </summary>
        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        /// <summary>
        /// Int value checking if the endpoint is connected and what value it hold -1,0,1
        /// </summary>
        public int In1
        {
            get
            {
                return this.in1;
            }
            set
            {
                this.in1 = value;
            }
        }
        /// <summary>
        /// Int value checking if the endpoint is connected and what value it hold -1,0,1
        /// </summary>
        public int In2
        {
            get
            {
                return this.in2;
            }
            set
            {
                this.in2 = value;
            }
        }
        /// <summary>
        /// Int value checking if the endpoint is connected and what value it hold -1,0,1
        /// </summary>
        public int Out
        {
            get
            {
                return this.out3;
            }
            set
            {
                //Calculate the result of the Gate based on in1 and in2
                this.out3 = value;
            }
        }
        /// <summary>
        /// See's what the current gate is connected too
        /// </summary>
        public int ParentGate
        {
            get
            {
                return this.nextGate;
            }
            set
            {
                this.nextGate = value;
            }
        }
        /// <summary>
        /// See's what the Point of the gate is.
        /// </summary>
        public Point Point
        {
            get
            {
                return this.point;
            }
            set
            {
                this.point = value;
            }
        }
        /// <summary>
        /// creates a new gate with a name passed by the parameter.
        /// </summary>
        /// <param name="name"></param>
        public Gate(string name)
        {
            ID = name;
        }
    }


}
