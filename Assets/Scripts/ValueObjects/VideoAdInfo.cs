using System.Xml;

public class VideoAdInfo
{
    public string url;

    public static VideoAdInfo FromXML(string data)
    {
        XmlDocument document = new XmlDocument();
        document.LoadXml(data);
        XmlNode node = document.SelectSingleNode("//MediaFile");

        if (node == null)
            throw new System.Exception();

        return new VideoAdInfo { url = node.InnerText };
    }
}