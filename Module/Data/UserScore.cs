﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Threading.Tasks;

namespace MopsBot.Module.Data
{
    class UserScore
    {
        public List<Individual.User> users = new List<Individual.User>();

        public UserScore()
        {
            #if NET40
                StreamReader read = new StreamReader("data//scores.txt");
            #else
                StreamReader read = new StreamReader(new FileStream("data//scores.txt",FileMode.Open));
            #endif
            

            string fs = "";
            while ((fs = read.ReadLine()) != null)
            {
                string[] s = fs.Split(':');
                users.Add(new Individual.User(ulong.Parse(s[0]),int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]), int.Parse(s[4]), int.Parse(s[5])));
            }

            #if NET40
                read.Close();
            #else
                read.Dispose();
            #endif

            users = users.OrderByDescending(u => u.Experience).ToList();
        }

        public void writeScore()
        {
            users = users.OrderByDescending(u => u.Experience).ToList();

            #if NET40
                StreamWriter write = new StreamWriter("data//scores.txt");
            #else
                StreamWriter write = new StreamWriter(new FileStream("data//scores.txt",FileMode.OpenOrCreate));
            #endif

            foreach (Individual.User that in users)
            {
                write.WriteLine($"{that.ID}:{that.Score}:{that.Experience}:{that.punched}:{that.hugged}:{that.kissed}");
            }

            #if NET40
                write.Close();
            #else  
                write.Dispose();
            #endif
        }

        public void addExperience(ulong id, int value)
        {
            users.Find(x => x.ID.Equals(id)).Experience += value;
            writeScore();
        }

        public string drawDiagram(int count, DiagramType type)
        {
            List<Individual.User> tempUsers = users.Take(count).ToList();

            int maximum = 0;
            string[] lines = new string[count];

            switch (type)
            {
                case DiagramType.Experience:
                    tempUsers = tempUsers.OrderByDescending(x => x.Experience).ToList();

                    maximum = tempUsers[0].Experience;

                    for (int i = 0; i < count; i++)
                    {
                        lines[i] = (i + 1).ToString().Length < 2 ? $"#{i + 1} |" : $"#{i + 1}|";
                        double relPercent = users[i].Experience / ((double)maximum / 10);
                        for (int j = 0; j < relPercent; j++)
                        {
                            lines[i] += "■";
                        }
                        lines[i] += $"  ({users[i].Experience} / {Program.client.GetUser(users[i].ID).Username})";
                    }
                    break;

                case DiagramType.Level:
                    tempUsers = tempUsers.OrderByDescending(x => x.Level).ToList();

                    maximum = tempUsers[0].Level;

                    for (int i = 0; i < count; i++)
                    {
                        lines[i] = (i + 1).ToString().Length < 2 ? $"#{i + 1} |" : $"#{i + 1}|";
                        double relPercent = users[i].Level / ((double)maximum / 10);
                        for (int j = 0; j < relPercent; j++)
                        {
                            lines[i] += "■";
                        }
                        lines[i] += $"  ({users[i].Level} / {Program.client.GetUser(users[i].ID).Username})";
                    }
                    break;

                case DiagramType.Score:
                    tempUsers = tempUsers.OrderByDescending(x => x.Score).ToList();

                    maximum = tempUsers[0].Score;

                    for (int i = 0; i < count; i++)
                    {
                        lines[i] = (i + 1).ToString().Length < 2 ? $"#{i+1} |" : $"#{i+1}|";
                        double relPercent = users[i].Score / ((double)maximum / 10);
                        for (int j = 0; j < relPercent; j++)
                        {
                            lines[i] += "■";
                        }
                        lines[i] += $"  ({users[i].Score} / {Program.client.GetUser(users[i].ID).Username})";
                    }
                    break;
            }

            string output = "```" + string.Join("\n", lines) + "```";

            return output;
        }

        public enum DiagramType{Experience, Level, Score}
    }
}
