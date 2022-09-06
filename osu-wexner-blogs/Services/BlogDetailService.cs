using Microsoft.VisualBasic.FileIO;
using osu_wexner_blogs.Model;
using System.Text;

namespace osu_wexner_blogs.Services
{
    public class BlogDetailService
    {
        static string Database = "../osu-wexner-blogs/Database/BlogDetails.csv";
        static List<BlogDetail> BlogDetails { get; } = LoadData();

        #region Create
        public static string Add(BlogDetail blogDetail)
        {
            if(Get(blogDetail.UUID) != null || blogDetail.UUID == null)
                blogDetail.UUID = Guid.NewGuid().ToString();

            Random rng = new Random();
            blogDetail.ReadTime = rng.Next(1, 100);
            blogDetail.Clicks = rng.Next(1, 10000);
            try
            {
                BlogDetails.Add(blogDetail);
                ReloadData();
            }
            catch (Exception e)
            {
                blogDetail.UUID = e.Message;
            }

            return blogDetail.UUID;
        }
        #endregion

        #region Read
        public static BlogDetail? Get(string pkey)
        {
            return BlogDetails.FirstOrDefault(bd => bd.UUID == pkey);
        }

        public static List<BlogDetail> GetAll()
        {
            return BlogDetails;
        }

        public static List<BlogDetail> GetByTopic(string topic)
        {
            return BlogDetails.FindAll(bd => bd.Topic == topic);
        }
        #endregion

        #region Update
        public static bool Update(BlogDetail blogDetail)
        {
            try
            {
                int index = BlogDetails.FindIndex(bd => bd.UUID == blogDetail.UUID);
                BlogDetails[index] = blogDetail;
                ReloadData();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region Delete
        public static bool Delete(BlogDetail blogDetail)
        {
            BlogDetail itemToRemove = Get(blogDetail.UUID);

            if (itemToRemove != null)
            {
                BlogDetails.Remove(itemToRemove);
                ReloadData();
                return true;
            }
            else
                return false;

        }
        #endregion

        #region Helpers
        public static List<BlogDetail> LoadData()
        {
            List<BlogDetail> list = new List<BlogDetail>();
            using (TextFieldParser parser = new TextFieldParser(Database))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    BlogDetail blogDetail = new BlogDetail();
                    if (fields.Length > 0 && fields[0] != "Pkey")
                    {
                        blogDetail.UUID = fields[0];
                        blogDetail.Title = fields[1];
                        blogDetail.Desc = fields[2];
                        blogDetail.AuthorName = fields[3];
                        blogDetail.Topic = fields[4];
                        blogDetail.PublishDate = DateTime.Parse(fields[5]);
                        blogDetail.ReadTime = long.Parse(fields[6]);
                        blogDetail.Clicks = int.Parse(fields[7]);
                        list.Add(blogDetail);
                    }
                }
            }
            return list;
        }

        public static void ReloadData()
        {
            StringBuilder csvBuilder = new StringBuilder();
            string headers = "Pkey,Title,Desc,AuthorName,Topic,PublishDate,ReadTime,Clicks";
            csvBuilder.AppendLine(headers);
            foreach (BlogDetail detail in BlogDetails)
            {
                csvBuilder.Append(detail.UUID + ",");
                csvBuilder.Append(detail.Title + ",");
                csvBuilder.Append(detail.Desc + ",");
                csvBuilder.Append(detail.AuthorName + ",");
                csvBuilder.Append(detail.Topic + ",");
                csvBuilder.Append(detail.PublishDate.ToString() + ",");
                csvBuilder.Append(detail.ReadTime + ",");
                csvBuilder.Append(detail.Clicks);
                csvBuilder.AppendLine("");
            }

            File.WriteAllText(Database, csvBuilder.ToString());
        }
        #endregion

    }
}
