using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// プラットフォームを考慮（windows,androidなど）して
/// StreamingAssetsからファイルを読み込む性的ユーティリティクラス
/// </summary>
public static class CSVReader
{

    public static async Task<string[][]> LoadCSVData(string csvFileName)
    {
        // 一文にする
        string filePath = Path.Combine(Application.streamingAssetsPath, csvFileName);
        string csvText = "";

#if UNITY_ANDROID && !UNITY_EDITOR
        // アンドロイドの端末であり、他のエディターではない場合の処理
        // UnityWebRequestを使用すると統一的に扱える
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        var operation = www.SendWebRequest();

        //リクエストが完了するまで待機
        while (!operation.isDone)
        {
            await Task.Yield();
        }

        if(www.result == UnityWebRequest.Result.Success)
        {
            csvText = www.downloadHandler.text;
        }
        else
        {
            // リクエストが失敗したら、どこの位置で失敗したかをデバッグで出力
            Debug.LogError("Failed to load CSV file '{csvFileName}' : {www.error}");
            return null;
        }
#else
        // pcまたは他のプラットフォーム
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found at path : {filePath}");
            return null;
        }

        try
        {
            // 同期的なファイル読み込みをバックグラウンドスレッドで実行し、メインスレッドのブロッキングを防ぐ
            csvText = await Task.Run(() => File.ReadAllText(filePath));
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load CSV file '{csvFileName}': {e.Message}");
            return null;
        }
#endif
        return ParseCSV(csvText);
    }

    /// <summary>
    /// CSV形式の文字列を解析し、stringのジャグ配列に変換する
    /// </summary>
    /// <param name="csvText"> 解析対象のCSV文字列 </param>
    /// <returns> 解析後のジャグ配列 string[][] </returns>
    private static string[][] ParseCSV(string csvText)
    {
        if (string.IsNullOrEmpty(csvText))
        {
            Debug.LogWarning("CSV data is empty or null");
            return new string[0][]; // 空の配列を返す
        }

        // 改行で各行に分割（空の行は無視する）
        string[] lines = csvText.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<string[]> data = new List<string[]>();

        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            // ダブルクォートで囲まれたカンマを無視する正規表現でフィールドに分割
            string[] fields = Regex.Split(line.Trim(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            List<string> remaining = new List<string>();

            // 各フィールドの前後の空白とダブルクォートを削除
            for (int i = 0; i < fields.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(fields[i])) continue;
                fields[i] = fields[i].Trim().Trim('"');
                remaining.Add(fields[i]);
            }
            data.Add(remaining.ToArray());
        }

        return data.ToArray();
    }
}
