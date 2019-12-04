using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummies
{
    public class Student : Person
    {
        public Student() : base() { }
        
        public Student(int nr, string name, int group, string githubId) : base(name)
        {
            this.Nr = nr;
            this.Group = group;
            this.GithubId = githubId;
        }

        public int Nr { get; set; }
        public int Group { get; set; }
        public string GithubId { get; set; }
        public double Grade { get; set; }
    }

}
