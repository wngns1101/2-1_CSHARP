﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week5_Homework
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        const int MAX_COUNT = 5;
        List<Car> cars = new List<Car>();

        bool IsEmptyCarNumber()
        {
            bool result = false;
            if (string.IsNullOrEmpty(tbxNumber.Text)) {
                MessageBox.Show("차량번호를 넣으세요.");
                result = true;
            }
            return result;
        }

        int SearchCar(string number)
        {
            int index = -1;
            for (int i = 0; i < cars.Count; i++) {
                if (cars[i] != null && cars[i].CarNumber == number) {
                    index = i;
                }
            }
            return index;
        }

        private void btnArrive_Click(object sender, EventArgs e)
        {
            #region code
            tbxView.Text = String.Empty;

            if (IsEmptyCarNumber()) {
                return;
            }

            if (-1 != SearchCar(tbxNumber.Text)) {
                MessageBox.Show("기존에 주차가 되어있는 차량입니다.");
                return;
            }

            if (cars.Count == MAX_COUNT) {
                MessageBox.Show("풀방");
                return;
            }

            Car car = null;
            if (rbtnShortTerm.Checked) {
                car = new ShortTermCar(tbxNumber.Text);
            } else {
                if (string.IsNullOrEmpty(tbxTerm.Text)) {
                    return;
                }

                if(false == int.TryParse(tbxTerm.Text, out int term)) {
                    return;
                }
                car = new LongTermCar(tbxNumber.Text, term);
            }
            
            cars.Add(car);

            tbxView.Text = car.ToString();
            tbxView.Text += Environment.NewLine;
            tbxView.Text += "입차 처리를 완료했습니다.";

            #endregion
        }

        private void btnDeparture_Click(object sender, EventArgs e)
        {
            #region code
            tbxView.Text = String.Empty;

            if (IsEmptyCarNumber()) {
                return;
            }

            int i = SearchCar(tbxNumber.Text);
            if (-1 == i) {
                MessageBox.Show("없다!");
                return;
            }
            var car = cars[i];
            car.Out();
            cars.RemoveAt(i);

            tbxView.Text = car.ToString();
            tbxView.Text += $"\r\n주차료:{car.GetPrice()}원";
            tbxView.Text += "출차 처리를 완료했습니다.";

            #endregion
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            #region code
            tbxView.Text = "[주차현황]\r\n\r\n";
            for (int i = 0; i < cars.Count; i++) {
                tbxView.Text += $"[{i + 1}] 주차상태 : ";
                tbxView.Text += cars[i];
                tbxView.Text += Environment.NewLine;
            }
            #endregion
        }

        private void rbtnShortTerm_CheckedChanged(object sender, EventArgs e)
        {
            pnlTermData.Visible = false;
        }

        private void rbtnlongTerm_CheckedChanged(object sender, EventArgs e)
        {
            pnlTermData.Visible = true;
        }
    }
}
