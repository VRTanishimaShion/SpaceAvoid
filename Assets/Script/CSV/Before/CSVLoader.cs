using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// CSVを読み込むためのクラス
/// </summary>
public static class CSVLoader
{
    /// <summary>
    /// CSVファイルを読み込む
    /// </summary>
    /// <param name="filePath">ファイルパス</param>
    /// <returns>行ごとにカンマ区切りで分割した文字列配列のリスト</returns>
    public static List<string[]> Load(string filePath)
    {
        var result = new List<string[]>();

        using (FileStream dataFile = new FileStream(Application.streamingAssetsPath + filePath, FileMode.Open, FileAccess.Read))
        {
            var reader = new StreamReader(dataFile);

            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                result.Add(line.Split(','));
            }
        }

        return result;
    }

    /// <summary>
    /// CSVの文字列をVector2に変換する
    /// </summary>
    /// <param name="csvValue">例: "1.2,3.4"</param>
    /// <returns>Vector2、変換できなければVector2.zero</returns>
    public static Vector2 ParseVector2(string csvValue)
    { 
        if (string.IsNullOrWhiteSpace(csvValue))
            return Vector2.zero;

        string[] parts = csvValue.Split('.');

        if (parts.Length != 2)
            return Vector2.zero;

        float x = 0f, y = 0f;
        float.TryParse(parts[0], out x);
        float.TryParse(parts[1], out y);

        return new Vector2(x, y);
    }

    /// <summary>
    /// CSVの文字列をVector3に変換する（必要なら追加可能）
    /// </summary>
    public static Vector3 ParseVector3(string csvValue)
    {
        if (string.IsNullOrWhiteSpace(csvValue))
            return Vector3.zero;

        string[] parts = csvValue.Split('.');

        if (parts.Length != 3)
            return Vector3.zero;

        float x = 0f, y = 0f, z = 0f;
        float.TryParse(parts[0], out x);
        float.TryParse(parts[1], out y);
        float.TryParse(parts[2], out z);

        return new Vector3(x, y, z);
    }
}
