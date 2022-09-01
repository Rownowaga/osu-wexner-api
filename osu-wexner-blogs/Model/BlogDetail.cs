namespace osu_wexner_blogs.Model
{
    public class BlogDetail
    {
        public string UUID { get; set; }
        public string Title { get; set; } = "";

        public string Desc { get; set; } = "";

        public string AuthorName { get; set; } = "";

        public string Topic { get; set; } = "";

        public DateTime PublishDate { get; set; } = DateTime.Now.Date;

        public long ReadTime { get; set; } = 0;

        public int Clicks { get; set; } = 0;
    }
}