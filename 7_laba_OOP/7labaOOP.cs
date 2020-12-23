﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _7_laba_OOP
{
	public partial class laba7 : Form
	{
		public laba7()
		{
			InitializeComponent();			
		}

		public class Figure
		{	// Composite
			public int x, y;
			public Color color = Color.Navy;
			public Color fillcolor = Color.White;

			public virtual void GroupAddFigure(ref Figure object1) { }
			public virtual void paint_figure(Pen pen, Brush figurefillcolor, Panel panel_drawing) { }
			public virtual void move_x(int x, Panel panel_drawing) { }
			public virtual void move_y(int y, Panel panel_drawing) { }
			public virtual void changesize(int size) { }
			public virtual bool checkfigure(int x, int y) { return false; }

		}
		class Group : Figure
		{
			public int maxcount = 10;
			public Figure[] group;
			public int count;
			public Group()
			{   // Выделяем maxcount мест в хранилище
				count = 0;
				group = new Figure[maxcount];
				for (int i = 0; i < maxcount; ++i)
					group[i] = null;
			}

			public override void GroupAddFigure(ref Figure object1)
            {
				if (count >= maxcount)
					return;
				group[count] = object1;
				++count;
			}
			public override void paint_figure(Pen pen, Brush figurefillcolor, Panel panel_drawing)
			{
				for(int i = 0; i < count; ++i)
                {
					group[i].paint_figure(pen, figurefillcolor, panel_drawing);
                }
			}

            public override void move_x(int x, Panel panel_drawing)
            {
				for (int i = 0; i < count; ++i)
				{
					group[i].move_x(x, panel_drawing);
				}
			}

			public override void move_y(int y, Panel panel_drawing)
			{
				for (int i = 0; i < count; ++i)
				{
					group[i].move_y(y, panel_drawing);
				}
			}
			public override void changesize(int size)
			{
				for (int i = 0; i < count; ++i)
				{
					group[i].changesize(size);
				}
			}

            public override bool checkfigure(int x, int y)
            {
                for(int i = 0; i < count; ++i)
                {
					if(group[i].checkfigure(x, y))
						return true;
                }
				return false;
            }
        }
		class Circle: Figure
		{
			public int rad = 30; // Радиус круга
			public Circle(int x, int y)
			{
				this.x = x - rad;
				this.y = y - rad;
			}
            public override void paint_figure(Pen pen, Brush figurefillcolor, Panel panel_drawing)
            {			
				panel_drawing.CreateGraphics().DrawEllipse(
					pen, x, y, rad * 2, rad * 2);
                panel_drawing.CreateGraphics().FillEllipse(
                    figurefillcolor, x, y, rad * 2, rad * 2);
            }
			public override void move_x(int x, Panel panel_drawing) 
			{
				int c = this.x + x;
				int gran = panel_drawing.ClientSize.Width - (rad * 2);
				check(c, x, gran, gran - 2, ref this.x);
			}
			public override void move_y(int y, Panel panel_drawing)
			{
				int c = this.y + y;
				int gran = panel_drawing.ClientSize.Height - (rad * 2);
				check(c, y, gran, gran - 2, ref this.y);
			}
			public override void changesize(int size)
            {
				rad += size;
			}
			public override bool checkfigure(int x, int y)
			{
				return ((x - this.x - rad) * (x - this.x - rad) + (y - this.y - rad) *
					(y - this.y - rad)) < (rad * rad);
			}
		}
		class Line: Figure
		{
			public int lenght = 60;
			public int wight = 5;
			public Line(int x, int y)
			{
				this.x = x - lenght/2;
				this.y = y;
			}
			public override void paint_figure(Pen pen, Brush figurefillcolor, Panel panel_drawing)
			{
				panel_drawing.CreateGraphics().DrawRectangle(pen, x,
										y, lenght, wight);
				panel_drawing.CreateGraphics().FillRectangle(figurefillcolor, x,
					y, lenght, wight);
			}
			public override void move_x(int x, Panel panel_drawing)
			{
				int l = this.x + x;
				int gran = panel_drawing.ClientSize.Width - lenght;
				check(l, x, gran, --gran, ref this.x);
			}
			public override void move_y(int y, Panel panel_drawing)
			{
				int l = this.y + y;
				int gran = panel_drawing.ClientSize.Height - wight;
				check(l, y, gran, --gran, ref this.y);
			}
			public override void changesize(int size)
			{
				lenght += size;
				wight += size / 5;
			}
			public override bool checkfigure(int x, int y)
			{
				return (this.x <= x && x <= (this.x + lenght) && (this.y - 2) <= y &&
									y <= (this.y + wight));
			}
		}
		class Square: Figure
		{
			public int size = 60;
			public Square(int x, int y)
			{
				this.x = x - size/2;
				this.y = y - size/2;
			}
			public override void paint_figure(Pen pen, Brush figurefillcolor, Panel panel_drawing)
			{
				panel_drawing.CreateGraphics().DrawRectangle(pen,
					x, y, size, size);
				panel_drawing.CreateGraphics().FillRectangle(figurefillcolor,
					x, y, size, size);
			}
			public override void move_x(int x, Panel panel_drawing)
			{
				int s = this.x + x;
				int gran = panel_drawing.ClientSize.Width - size;
				check(s, x, gran, --gran, ref this.x);
			}
			public override void move_y(int y, Panel panel_drawing)
			{
				int s = this.y + y;
				int gran = panel_drawing.ClientSize.Height - size;
				check(s, y, gran, --gran, ref this.y);
			}
			public override void changesize(int size)
			{
				this.size += size;
			}
			public override bool checkfigure(int x, int y)
			{
				return (this.x <= x && x <= (this.x + size) &&
										this.y <= y && y <= (this.y + size));
			}
		}
		static public void check(int f, int chislo, int gran, int gran1, ref int x)
		{   // Проверка на выход фигуры за границы
			if (f > 0 && f < gran)
				x += chislo;
			else
			{
				if (f <= 0)
					x = 1;
				else
					if (f >= gran1)
					x = gran1;
			}
		}

		int p = 0; // Нажат ли был ранее Ctrl
		static int k = 5; // Кол-во ячеек в хранилище
		Storage storag = new Storage(k); // Создаем объект хранилища
		static int index = 0; // Кол-во нарисованных кругов
		int indexin = 0; // Индекс, в какое место был помещён круг
		int figure_now = 1;	// Какая фигура выбрана

		class Storage
		{
			public Figure[] objects;
			public Storage(int count)
			{   // Выделяем count мест в хранилище
				objects = new Figure[count];
				for (int i = 0; i < count; ++i)
					objects[i] = null;
			}
			public void initialisat(int count)
			{   // Выделяем count мест в хранилище
				objects = new Figure[count];
				for (int i = 0; i < count; ++i)
					objects[i] = null;
			}
			public void add_object(int ind, ref Figure object1, int count, ref int indexin)
			{   // Добавляет ячейку в хранилище
				// Если ячейка занята ищем свободное место
				while (objects[ind] != null)
				{
					ind = (ind + 1) % count;
				}
				objects[ind] = object1;
				indexin = ind;
			}
			public void delete_object(int ind)
			{   // Удаляет объект из хранилища
				objects[ind] = null;
                if (index > 0)
					index--;
			}
			public bool check_empty(int index)
			{   // Проверяет занято ли место хранилище
				if (objects[index] == null)
					return true;
				else return false;
			}
			public int occupied(int size)
			{ // Определяет кол-во занятых мест в хранилище
				int count_occupied = 0;
				for (int i = 0; i < size; ++i)
					if (!check_empty(i))
						++count_occupied;
				return count_occupied;
			}
			public void doubleSize(ref int size)
			{   // Функция для увеличения кол-ва элементов в хранилище в 2 раза 
				Storage storage1 = new Storage(size * 2);
				for (int i = 0; i < size; ++i)
					storage1.objects[i] = objects[i];
				for (int i = size; i < (size * 2) - 1; ++i)
					storage1.objects[i] = null;
				size = size * 2;
				initialisat(size);
				for (int i = 0; i < size; ++i)
					objects[i] = storage1.objects[i];
			}
			~Storage() { }
		};
		private void panel_drawing_MouseClick(object sender, MouseEventArgs e)
		{
			//Проверка на наличие фигуры на данных координатах
			int c = check_figure(ref storag, k, e.X, e.Y);
			if (c != -1)
			{   // Если на этом месте уже нарисована фигура
				if (Control.ModifierKeys == Keys.Control)
				{   // Если нажат ctrl, то выделяем несколько объектов
					if (p == 0)
					{
						paint_figure(Color.Navy, 4, ref storag, indexin);
						p = 1;
					}
					// Вызываем функцию отрисовки фигуры  
					paint_figure(Color.Red, 4, ref storag, c);
				}
				else
				{   // Иначе выделяем только один объект
					// Снимаем выделение у всех объектов хранилища
					remove_selection_circle(ref storag); 
					paint_figure(Color.Red, 4, ref storag, c);
				}
				return;
			}
			Figure figure = new Figure();
			switch (figure_now)
			{   // В зависимости какая фигура выбрана
				case 0:
					return;
				case 1:
					figure = new Circle(e.X, e.Y);
					break;
				case 2:
					figure = new Line(e.X, e.Y);
					break;
				case 3:
					figure = new Square(e.X, e.Y);
					break;
			}
			if (index == k)
				storag.doubleSize(ref k);
			// Добавляем фигуру в хранилище   
			storag.add_object(index, ref figure, k, ref indexin);
			// Снимаем выделение у всех объектов хранилища
			remove_selection_circle(ref storag);
			storag.objects[indexin].fillcolor = colorDialog1.Color;
			paint_figure(Color.Red, 4, ref storag, indexin);
			++index;
			p = 0;
		}
		private void remove_selection_circle(ref Storage stg)
		{   // Снимает выделение у всех элементов хранилища
			for (int i = 0; i < k; ++i)
			{
				if (!stg.check_empty(i))
				{   // Вызываем функцию отрисовки круга
					paint_figure(Color.Navy, 4, ref storag, i);
				}
			}
		}
		private void move_y(ref Storage stg, int y)
		{	// Функция для перемещения фигур по оси Y
			for(int i = 0; i < k; ++i)
			{
				if (!stg.check_empty(i))
				{
					if(stg.objects[i].color == Color.Red)
                    {   // Если объект выделен
						stg.objects[i].move_y(y, panel_drawing);
					}
				}
			}
		}
        private void move_x(ref Storage stg, int x)
		{   // Функция для перемещения фигур по оси X
			for (int i = 0; i < k; ++i)
            {
                if (!stg.check_empty(i))
                {
                    if (stg.objects[i].color == Color.Red)
					{   // Если объект выделен
						stg.objects[i].move_x(x, panel_drawing);
                    }
                }
            }
        }
		private void changesize(ref Storage stg, int size)
		{	// Увеличивает или уменьшает размер фигур, в зависимости от size
			for (int i = 0; i < k; ++i)
			{
				if (!stg.check_empty(i))
				{   // Если под i индексом в хранилище есть объект
					if (stg.objects[i].color == Color.Red)
					{
						stg.objects[i].changesize(size);
					}
				}
			}
		}
        private void remove_selected_circle(ref Storage stg)
		{   // Удаляет выделенные элементы из хранилища
			for (int i = 0; i < k; ++i)
			{
				if (!stg.check_empty(i))
				{
					if (stg.objects[i].color == Color.Red)
						stg.delete_object(i);
				}
			}
		}
		private void paint_figure(Color name, int size, ref Storage stg, int index)
		{   // Рисует фигуру на панели          
			// Объявляем объект - карандаш, которым будем рисовать контур
			if (!stg.check_empty(index))
			{
				Pen pen = new Pen(name, size);
				stg.objects[index].color = name;
				SolidBrush figurefillcolor = new SolidBrush(stg.objects[index].fillcolor);
				stg.objects[index].paint_figure(pen, figurefillcolor, panel_drawing);                
            }
		}
		private void paint_all(ref Storage stg)
        {	// Рисует все фигуры на панели
			for (int i = 0; i < k; ++i)
				if (!stg.check_empty(i))
					paint_figure(stg.objects[i].color, 4, ref storag, i);
		}
		private int check_figure(ref Storage stg, int size, int x, int y)
		{   // Проверяет есть ли уже фигура с такими же координатами в хранилище
			if (stg.occupied(size) != 0)
			{
				for (int i = 0; i < size; ++i)
				{
					if (!stg.check_empty(i))
					{   // Если под i индексом в хранилище есть объект
						if (stg.objects[i].checkfigure(x, y))
							return i;					   
					}
				}
			}
			return -1;
		}

		private void drawellipse_Click(object sender, EventArgs e)
		{
			drawline.Checked = false;
			drawsquare.Checked = false;
			figure_now = 1;			
			if (drawellipse.Checked == false) // Если не выбрана фигура
				figure_now = 0;
		}

		private void drawline_Click(object sender, EventArgs e)
		{
			drawsquare.Checked = false;
			drawellipse.Checked = false;
			figure_now = 2;
			if (drawline.Checked == false) // Если не выбрана фигура
				figure_now = 0;
		}

		private void drawsquare_Click(object sender, EventArgs e)
		{
			drawline.Checked = false;
			drawellipse.Checked = false;
			figure_now = 3;
			if (drawsquare.Checked == false) // Если не выбрана фигура
				figure_now = 0;
		}

		private void laba7_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Delete)
			{   // Удаление выделенных фигур, если нажата кнопка delete
				remove_selected_circle(ref storag);
			}
			if(e.KeyCode == Keys.W)
			{	// Перемещение по оси X вверх
				move_y(ref storag, -10);
			}
			if (e.KeyCode == Keys.S)
			{	// Перемещение по оси X вниз
				move_y(ref storag, +10);
			}
			if (e.KeyCode == Keys.A)
			{	// Перемещение по оси Y вниз
				move_x(ref storag, -10);
			}
			if (e.KeyCode == Keys.D)
			{   // Перемещение по оси Y вверх
				move_x(ref storag, +10);
			}
			if (e.KeyCode == Keys.Oemplus)
			{	// Увеличиваем размер фигуры
				changesize(ref storag, 10);
			}
			if (e.KeyCode == Keys.OemMinus)
			{	// Уменьшаем размер фигуры
				changesize(ref storag, -10);
			}
			panel_drawing.Refresh();
			paint_all(ref storag);
		}

        private void btn_select_color_Click(object sender, EventArgs e)
        {	// Смена цвета у фигур
			if (colorDialog1.ShowDialog() == DialogResult.Cancel)
				return;
			btn_select_color.BackColor = colorDialog1.Color;
			for (int i = 0; i < k; ++i)
			{
				if (!storag.check_empty(i))
					if (storag.objects[i].color == Color.Red)
					{
						storag.objects[i].fillcolor = colorDialog1.Color;
						paint_figure(storag.objects[i].color, 4, ref storag, i);
					}
			}
		}

        private void btn_group_Click(object sender, EventArgs e)
        {
			Figure group = new Group();
			for(int i = 0; i < k; ++i)
            {
				if (!storag.check_empty(i))
					if (storag.objects[i].color == Color.Red)
					{
						group.GroupAddFigure(ref storag.objects[i]);
						storag.delete_object(i);
					}
			}
			storag.add_object(index, ref group, k, ref indexin);
        }
    }
}