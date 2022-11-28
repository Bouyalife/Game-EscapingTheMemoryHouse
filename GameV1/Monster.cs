using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameV1
{
    internal class Monster : PictureBox
    {
        public Monster() {
            this.health = 150;
            this.speed = 1;
            this.walk = true;
        }

      
        private int health;
        private int speed;
        private Boolean walk;

        public int getHealth()
        {
            return this.health;
        }
        public int getSpeed() {
            return this.speed;
        }

        public void setSpeed(int speed)
        {
            this.speed = speed;
        }
        public void setHealth(int health)
        {
            this.health -= health;
        }

        public void setWalk()
        {
            walk = !walk;
        }

        public Boolean getWalk()
        {
            return this.walk;
        }
    }
}
