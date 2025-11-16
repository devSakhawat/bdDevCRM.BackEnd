namespace Utilities.Model
{
  public class ReportFileParam<T>
  {

    public string ReportFileName { get; set; }
    public string ReportTitle { get; set; }
    public string BaseFolderPath { get; set; }
    public int ReportHeaderId { get; set; }
    public string ReportFileKey { get; set; }
    public List<T> DataSource { get; set; }
    public string ExportPath { get; set; }
    public string ExportFileName { get; set; }




    public bool IsParamAllow { get; set; }
  }
}
