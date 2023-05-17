using System;

[Serializable]
public class LocaleString
{

    //TODO: Придумать что-то с локализацией. Юнити не даёт нормальной возможности вывести в инспектор хэш таблицу
    public string rusValue;
    public string engValue;

    public string GetValue()
    {
        switch(LocaleMaster.Instance.currentLocale)
        {
            case GameLocale.Russian:
                return rusValue;
            case GameLocale.English:
                return engValue;
            default:
                return rusValue;
        }
    }
}
