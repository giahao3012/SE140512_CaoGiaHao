using SongAssemblies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmSong
{
    public partial class frmSongMain : Form
    {
        DataTable dtSong;
        SongDAO dao = new SongDAO();
        public frmSongMain()
        {
            InitializeComponent();
            CenterToScreen();
        }
        private void loadData()
        {
            lbStatus.Text = "Ready...";
            dtSong = dao.getSongs();
            dtSong.PrimaryKey = new DataColumn[] { dtSong.Columns["SongID"] };

            txtSongID.DataBindings.Clear();
            txtSongName.DataBindings.Clear();
            txtDuration.DataBindings.Clear();
            txtSinger.DataBindings.Clear();

            txtSongID.DataBindings.Add("Text", dtSong, "SongID");
            txtSongName.DataBindings.Add("Text", dtSong, "SongName");
            txtDuration.DataBindings.Add("Text", dtSong, "Duration");
            txtSinger.DataBindings.Add("Text", dtSong, "Singer");

            dgvSongList.DataSource = dtSong;
        }
        private void frmSongMain_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Ready...";
            txtSongID.DataBindings.Clear();
            txtSongName.DataBindings.Clear();
            txtDuration.DataBindings.Clear();
            txtSinger.DataBindings.Clear();

            txtSongID.Text = "";
            txtSongName.Text = "";
            txtDuration.Text = "";
            txtSinger.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            lbStatus.Text = "Creating...";
            int SongID = 0;
            if (!int.TryParse(txtSongID.Text, out SongID))
            {
                MessageBox.Show("Song ID must be filled in number type.");
                return;
            }
            if (SongID <= 0)
            {
                MessageBox.Show("How can you think an ID might be " + SongID + " ???????");
                return;
            }
            if (dao.findSongbyID(SongID) != 0)
            {
                MessageBox.Show("Song ID already existed.");
                return;
            }
            string SongName = txtSongName.Text;
            if (SongName == string.Empty)
            {
                MessageBox.Show("Fullname can not be empty.");
                return;
            }
            int Duration = 0;
            if (!int.TryParse(txtDuration.Text, out Duration))
            {
                MessageBox.Show("Duration must be a number.");
                return;
            }
            if (Duration <= 0)
            {
                MessageBox.Show("Duration must greater than 0.");
                return;
            }
            string Singer = txtSinger.Text;
            if (Singer == string.Empty)
            {
                MessageBox.Show("Singer can not be empty.");
                return;
            }

            Song song = new Song {
                SongID = SongID,
                SongName=SongName,
                Duration=Duration,
                Singer=Singer
            };

            if (dao.addSong(song))
            {
                MessageBox.Show("Add Sucessfull.");
                loadData();
            }
            else
            {
                MessageBox.Show("Add Failed.");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Updating...";
            int SongID = 0;
            if (!int.TryParse(txtSongID.Text, out SongID))
            {
                MessageBox.Show("Song ID must be filled in number type.");
                return;
            }
            if (SongID <= 0)
            {
                MessageBox.Show("How can you think an ID might be " + SongID + " ???????");
                return;
            }
            if (dao.findSongbyID(SongID) == 0)
            {
                MessageBox.Show("Song ID is not existed.");
                return;
            }
            string SongName = txtSongName.Text;
            if (SongName == string.Empty)
            {
                MessageBox.Show("Fullname can not be empty.");
                return;
            }
            int Duration = 0;
            if (!int.TryParse(txtDuration.Text, out Duration))
            {
                MessageBox.Show("Duration must be a number.");
                return;
            }
            if (Duration <= 0)
            {
                MessageBox.Show("Duration must greater than 0.");
                return;
            }
            string Singer = txtSinger.Text;
            if (Singer == string.Empty)
            {
                MessageBox.Show("Singer can not be empty.");
                return;
            }

            Song song = new Song
            {
                SongID = SongID,
                SongName = SongName,
                Duration = Duration,
                Singer = Singer
            };

            if (dao.updateSong(song))
            {
                MessageBox.Show("Update Sucessfull.");
                loadData();
            }
            else
            {
                MessageBox.Show("Add Failed.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbStatus.Text = "Searching...";
            string Search = txtSearch.Text;
            if (Search == string.Empty)
            {
                MessageBox.Show("You not provide anything. How can I search?????");
                return;
            }
            dtSong = dao.findSongbyName(Search);
            if (dtSong != null && dtSong.Rows.Count != 0) {
                txtSongID.DataBindings.Clear();
                txtSongName.DataBindings.Clear();
                txtDuration.DataBindings.Clear();
                txtSinger.DataBindings.Clear();

                txtSongID.DataBindings.Add("Text", dtSong, "SongID");
                txtSongName.DataBindings.Add("Text", dtSong, "SongName");
                txtDuration.DataBindings.Add("Text", dtSong, "Duration");
                txtSinger.DataBindings.Add("Text", dtSong, "Singer");

                dgvSongList.DataSource = dtSong;
            }
            else
            {
                MessageBox.Show("Can not found your song.");
                
            }
        }
    }
}
