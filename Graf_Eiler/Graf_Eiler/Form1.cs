using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graf_Eiler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Graf
        {
            public struct Vertex
            {
                string date;

                public Vertex(string new_date)
                {
                    this.date = new_date;
                }
                public void setVertexData(string new_date)
                {
                    this.date = new_date;
                }
                public string getVertexData()
                {
                    return this.date;
                }
            }
            public struct Edge
            {
                Vertex stockVertex;
                Vertex sourceVertex;
                string inf;
                public Edge(Vertex new_stockVertex, Vertex new_sourceVertex)
                {
                    this.stockVertex = new_stockVertex;
                    this.sourceVertex = new_sourceVertex;
                    this.inf = null;
                }
                public Edge(Vertex new_stockVertex, Vertex new_sourceVertex, string new_inf)
                {
                    this.stockVertex = new_stockVertex;
                    this.sourceVertex = new_sourceVertex;
                    this.inf = new_inf;
                }
                public void setStockVertex(Vertex new_stockVertex)
                {
                    this.stockVertex = new_stockVertex;
                }
                public Vertex getStockVertex()
                {
                    return this.stockVertex;
                }
                public void setSourceVertex(Vertex new_sourceVertex)
                {
                    this.sourceVertex = new_sourceVertex;
                }
                public Vertex getSourceVertex()
                {
                    return this.sourceVertex;
                }
                public void setInf(string new_inf)
                {
                    this.inf = new_inf;
                }
                public string getInf()
                {
                    return this.inf;
                }
            }
            List<Vertex> list_vertex;
            List<Edge> list_edge;

            public void add_to_edge_list(Edge edge_to_add)
            {
                list_edge.Add(edge_to_add);
            }
            public void del_from_edge_list(string inf_to_del)
            {
                foreach (Edge e in list_edge)
                {
                    if (e.getInf() == inf_to_del)
                    {
                        list_edge.Remove(e);
                    }
                }
            }
            /*
            public Edge find_from_edge_list(string inf_to_find)
            {
                foreach (Edge e in list_edge)
                {
                    if (e.getInf() == inf_to_find)
                    {
                        return e;
                    }
                }
            }
            */
            public void add_to_vertex_list(Vertex vertex_to_add)
            {
                list_vertex.Add(vertex_to_add);
            }
            public void del_from_vertex_list(string date_to_del)
            {
                foreach (Vertex v in list_vertex)
                {
                    if (v.getVertexData() == date_to_del)
                    {
                        list_vertex.Remove(v);
                    }
                }
            }
            /*
            public Vertex find_from_vertex_list(string date_to_find)
            {
                foreach (Vertex v in list_vertex)
                {
                    if (v.getVertexData() == date_to_find)
                    {
                        return v;
                    }
                }
            }
            */
            //Описать для стока и истока
            /*
             
             
             
             
             
             */
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string matSt = richTextBox1.Text;
            String[] str = matSt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на строки
            int n = str.Length;
            int z = 0;
            String[,] mass = new string[n, n];
            String[] pointsFF = matSt.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);//Разбиваем матрицу на символы
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    mass[i, j] = pointsFF[z];
                    z++;
                }
            }
            int[] prov_mass = new int[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mass[i, j] != "0")
                        prov_mass[i]++;
                }
            }
            int chetchik = 0;
            for (int i = 0; i < n; i++)
            {
                if (prov_mass[i]%2==1)
                    chetchik++;
            }
            if (chetchik==2)
                label2.Text = "Граф является\nполуэйлеровым!";
            else
                label2.Text = "Граф НЕ является\nполуэйлеровым!";
            //MessageBox.Show("Граф является полуэйлеровым!");
        }

    }
}
