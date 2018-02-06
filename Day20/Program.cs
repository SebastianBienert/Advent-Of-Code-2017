using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Vector
    {
        public Vector(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }


        public static bool operator== (Vector a, Vector b)
        {
            return !(a != b);
        }

        public static bool operator !=(Vector a, Vector b)
        {
            if (a.X != b.X)
                return true;
            if (a.Y != b.Y)
                return true;
            if (a.Z != b.Z)
                return true;

            return false;
        }
    }

    class Particle
    {
        public Vector Position { get; set; }
        public Vector Speed { get; set; }
        public Vector Acceleration { get; set; }

        public void Update()
        {
            Speed.X += Acceleration.X;
            Speed.Y += Acceleration.Y;
            Speed.Z += Acceleration.Z;

            Position.X += Speed.X;
            Position.Y += Speed.Y;
            Position.Z += Speed.Z;
        }

        public int GetDistance()
        {
            var result = Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);
            return result;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllLines("D:/data20.txt").ToList();
            char[] separators = {'=', ',', '<', '>', 'p', 'a', 'v', ' '};
            List<Particle> particles = new List<Particle>();
            foreach (var line in data)
            {
               var splited = line.Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
               var pos = new Vector(splited[0], splited[1], splited[2]);
               var vel = new Vector(splited[3], splited[4], splited[5]);
               var acc = new Vector(splited[6], splited[7], splited[8]);

                var particle = new Particle()
                {
                    Position = pos,
                    Speed = vel,
                    Acceleration = acc
                };
                particles.Add(particle);

            }

            /*for (int i = 0; i < 1000; i++)
            {
                int min = Int32.MaxValue;
                int index = 0;
                for (int j = 0; j < particles.Count; j++)
                {
                    var dist = particles[j].GetDistance();
                    if (dist < min)
                    {
                        min = dist;
                        index = j;
                    }
                }
                Console.WriteLine($"Particle {index} is nearest, distance {min}");

                foreach (var particle in particles)
                {
                    particle.Update();
                }
            }*/
            List<Particle> particlesToRemove = new List<Particle>();
            for (int rep = 0; rep < 10000; rep++)
            {
                foreach (var particle in particles)
                {
                    particle.Update();
                }
                for (int i = 0; i < particles.Count; i++)
                {
                    particlesToRemove.Clear();
                    bool collision = false;
                    for (int j = 0; j < particles.Count; j++)
                    {
                        if (particles[i].Position == particles[j].Position && i != j)
                        {
                            particlesToRemove.Add(particles[j]);
                            collision = true;
                        }
                    }
                    if (collision)
                    {
                        particles.Remove(particles[i]);
                        foreach (var part in particlesToRemove)
                        {
                            particles.Remove(part);
                        }
                    }

                }


            }
            Console.WriteLine(particles.Count);
            Console.ReadKey();
        }
    }
}
