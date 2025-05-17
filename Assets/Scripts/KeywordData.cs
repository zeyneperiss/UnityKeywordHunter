using UnityEngine;

public class KeywordData : MonoBehaviour
{
    public string keywordName;
    public string correctDropArea;

    public bool IsCorrectlyDropped()
    {
        if (transform.parent == null) return false;

        // Drop alanının adını kontrol et
        return transform.parent.name == correctDropArea;
    }
}

//public class KeywordData : MonoBehaviour
//{
//    public string correctDropArea; // ✅ Bu satır eksikse hata alırsın

//    public bool IsCorrectlyDropped()
//    {
//        if (transform.parent == null)
//            return false;

//        return transform.parent.name == correctDropArea;
//    }
//}
