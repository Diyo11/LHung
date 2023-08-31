
public interface ISaveable
{
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }


    GameSaveData GenerateSaveData();        //生成儲存數據

    void RestoreGameData(GameSaveData saveData);     //還原數據
    
}
