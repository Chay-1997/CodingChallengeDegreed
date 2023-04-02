using ClientDadCallJokes;

namespace CallDadJokes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private ClientDadCallJokes.ClientDadCallJokes clientDadCallJokes;
        private string searchTerm = string.Empty;
        private void Form1_Load(object sender, EventArgs e)
        {
            clientDadCallJokes=new ClientDadCallJokes.ClientDadCallJokes();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var res =  await clientDadCallJokes.GetRandomJokeAsync();
            richTextBox1.Text += res;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();

            if (searchTerm != string.Empty) {
                var res = await clientDadCallJokes.GetJokesAsync(searchTerm);
                foreach (var item in res)
                {
                    var key=item.Key;
                    var values = String.Join(',',item.Value);
                    richTextBox1.Text += key+"=>"+values;
                }
            }
            searchTerm = string.Empty;
            textBox1.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            searchTerm = textBox?.Text;
        }
    }
}