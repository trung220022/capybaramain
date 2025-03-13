﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreCoin : MonoBehaviour
{
    public static ScoreCoin instance; // Singleton để dễ dàng truy cập
    public Text coinText; // Text để hiển thị số coin trên UI
    private int coins = 0; // Số lượng coin ban đầu là 0

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadCoins(); // Tải số lượng coin khi khởi tạo
        }
    }

    void Start()
    {
        UpdateCoinText(); // Cập nhật UI để hiển thị số coin ngay khi bắt đầu
    }

    // Hàm để tăng coin
    public void AddCoins(int amount)
    {
        coins += amount; // Tăng số lượng coin
        SaveCoins(); // Lưu số lượng coin mỗi khi thêm
        UpdateCoinText(); // Cập nhật UI
    }

    // ✅ Hàm để trừ vàng khi ấn vào button
    public bool SpendCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount; // Trừ vàng
            SaveCoins(); // Lưu số vàng sau khi trừ
            UpdateCoinText(); // Cập nhật UI
            return true; // Trừ thành công
        }
        else
        {
            Debug.Log("❌ Không đủ vàng để mở khóa!");
            return false; // Không đủ vàng
        }
    }

    // Hàm để cập nhật UI
    private void UpdateCoinText()
    {
        coinText.text = " " + coins.ToString(); // Hiển thị số vàng
    }

    // Lưu số lượng coin vào PlayerPrefs
    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.Save();
    }

    // Tải số lượng coin từ PlayerPrefs
    private void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
    }

    // Hàm để reset coin nếu cần
    public void ResetCoins()
    {
        coins = 0;
        SaveCoins();
        UpdateCoinText();
    }

    // Hàm để lấy số lượng coin hiện tại
    public int GetCoinCount()
    {
        return coins;
    }
}
