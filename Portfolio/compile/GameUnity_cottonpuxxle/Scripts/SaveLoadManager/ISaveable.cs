
public interface ISaveable
{
    void SaveableRegister()
    {
        SaveLoadManager.Instance.Register(this);
    }


    GameSaveData GenerateSaveData();        //�ͦ��x�s�ƾ�

    void RestoreGameData(GameSaveData saveData);     //�٭�ƾ�
    
}
