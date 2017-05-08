using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TTSSLib.Interfaces;
using TTSSLib.Services;

namespace TTSSXTest
{
    public partial class Form1 : Form
    {
        IStopService _stopService;
        IPassageService _passageService;

        public Form1()
        {
            InitializeComponent();
            _stopService = new StopService();
            _passageService = new PassageService();
        }

        private async void tbAutoComplete_TextChanged(object sender, EventArgs e)
        {
            lbStopService.DataSource = await _stopService.GetCompletionFromService(tbAutoComplete.Text);
        }

        private async void btAll_Click(object sender, EventArgs e)
        {
            lbStopService.DataSource = await _stopService.GetAllStops();
        }

        private async void btLoadPassages_Click(object sender, EventArgs e)
        {
            var pas = await _passageService.GetPassages(int.Parse(tbPAssagesSID.Text));
            lbPassagesAct.DataSource = pas.ActualPassages;
            lbPassagesOld.DataSource = pas.OldPassages;
        }
    }
}
