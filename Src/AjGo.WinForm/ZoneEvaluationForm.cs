using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using AjGo;
using AjGo.Evaluators;

namespace AjGo.WinForm
{
    public partial class ZoneEvaluationForm : Form
    {
        public ZoneEvaluationForm(ZoneEvaluation evaluation)
        {
            InitializeComponent();
            lblColor.Text = evaluation.Color.ToString();
            lblSize.Text = evaluation.Size.ToString();
            lblInternals.Text = evaluation.InternalCount.ToString();
            txtGreenLife.Text = evaluation.GreenLife.ToString();
            txtGreenEyes.Text = evaluation.GreenEyes.ToString();
            txtBlueEyes.Text = evaluation.BlueEyes.ToString();
            txtStoneGroups.Text = evaluation.NGroups.ToString();
            txtSafeGroups.Text = evaluation.NSafeGroups.ToString();
            txtTrueEyes.Text = evaluation.TrueEyes.ToString();
            txtPointValue.Text = evaluation.PointValue.ToString();
        }
    }
}