using LibrarySystem.Application.DTOs.Users;
using LibrarySystem.Application.Services.Interfaces;

namespace LibrarySystem.WinForms.Forms.Users
{
    public partial class UsersForm : Form
    {
        private readonly IUserService _userService;

        public UsersForm(IUserService userService)
        {
            InitializeComponent();
            _userService = userService;
        }

        private async void UsersForm_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }

        private async Task LoadUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync(CancellationToken.None);
            dataGridViewUsers.DataSource = users.ToList();
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            using var form = new AddUserForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var request = new CreateUserRequest
                {
                    Name = form.UserName
                };

                await _userService.CreateUserAsync(request, CancellationToken.None);
                await LoadUsersAsync();
            }
        }
	}
}