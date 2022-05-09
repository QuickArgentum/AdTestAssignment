using System.Xml;

public class VideoAd
{
    public string url;

    public static VideoAd FromXML(string data)
    {
        XmlDocument document = new XmlDocument();
        document.LoadXml(data);
        XmlNode node = document.SelectSingleNode("//MediaFile");

        return new VideoAd { url = node?.InnerText };
    }
}