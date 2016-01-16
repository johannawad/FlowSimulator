using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FlowSimulator
{
   public abstract class Part :Component
    {
        //everything that was in component and was only used by the parts is now in parts


        protected Image compImage, compImageNot, compIcon, compIconNot;

        /// <summary>
        /// sets the base for setting it's position
        /// </summary>
        /// <param name="_position">A point where it is stated</param>
        public Part(Point _position)  {
           
            this.position = _position;
            Size s = new Size(40, 40);
            selectionArea = new Rectangle(this.Position, new Size(40, 40));
        }

        /// <summary>
        /// the InPut Port OutPut Port and so on
        /// </summary>
        public enum Port
        {
            InPut, OutPut, InPutUp, InPutDown, OutPutUp, OutPutDown,
        }
        /// <summary>
        /// it checks to know where the pipe is connected to the part
        /// </summary>
        /// <param name="p">the pipe in question</param>
        public abstract Port WhichPort(Pipeline p);
        /// <summary>
        /// To establish a connection between a Part and a pipeline
        /// </summary>
        /// <returns></returns>
        public abstract void Connect(Pipeline p);
        /// <summary>
        /// This is for disconnecting a pipe from the part
        /// </summary>
        /// <param name="p">the pipe that should be removed from the component</param>
        public abstract void Disconnect(Pipeline p);
        public abstract Image ComponentImage(bool isOccupied);
        public abstract Image ComponentIconImage(bool isOccupied);

    }
}
