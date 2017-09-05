using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanelController : MonoBehaviour
{
    [SerializeField] private Text matterCurrent;
    [SerializeField] private Text matterMax;
    [SerializeField] private Text energyCurrent;
    [SerializeField] private Text energyMax;

    private bool isInit = false;

    public int MatterCurrent
    {
        get
        {
            int value = 0;
            int.TryParse(matterCurrent.text, out value);
            return value;
        }
        set
        {
            matterCurrent.text = value.ToString();
        }
    }
    public int MatterMax
    {
        get
        {
            int value = 0;
            int.TryParse(matterMax.text, out value);
            return value;
        }
        set
        {
            matterMax.text = value.ToString();
        }
    }
    public int EnergyCurrent
    {
        get
        {
            int value = 0;
            int.TryParse(energyCurrent.text, out value);
            return value;
        }
        set
        {
            energyCurrent.text = value.ToString();
        }
    }
    public int EnergyMax
    {
        get
        {
            int value = 0;
            int.TryParse(energyMax.text, out value);
            return value;
        }
        set
        {
            energyMax.text = value.ToString();
        }
    }
    
    [SerializeField] private Color colorWarnLow = Color.red;
    [SerializeField] private Color colorWarnNormal = Color.gray;
    [SerializeField] private Color colorWarnMax = Color.green;
    // Use this for initialization
    void Start()
    {
        Init();
    }

    public void Init()
    {
        if(! isInit)
        {
            GameManager.Instance.EventHub.MatterCurrentChangeEvent += new ResourceChangeHandler(MatterCurrentSet);
            GameManager.Instance.EventHub.MatterMaxChangeEvent += new ResourceChangeHandler(MatterMaxSet);
            GameManager.Instance.EventHub.EnergyCurrentChangeEvent += new ResourceChangeHandler(EnergyCurrentSet);
            GameManager.Instance.EventHub.EnergyMaxChangeEvent += new ResourceChangeHandler(EnergyMaxSet);

            GameManager.Instance.EventHub.MatterLow += new ResourceWarnHandler(MatterLow);
            GameManager.Instance.EventHub.MatterMaxed += new ResourceWarnHandler(MatterMaxed);
            GameManager.Instance.EventHub.EnergyLow += new ResourceWarnHandler(EnergyLow);
            GameManager.Instance.EventHub.EnergyMaxed += new ResourceWarnHandler(EnergyMaxed);
            isInit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MatterCurrentSet(int value)
    {
        MatterCurrent = value;
    }
    void MatterMaxSet(int value)
    {
        MatterMax = value;
    }
    void EnergyCurrentSet(int value)
    {
        EnergyCurrent = value;
    }
    void EnergyMaxSet(int value)
    {
        EnergyMax = value;
    }

    void MatterLow(bool isLow)
    {
        if(isLow)
        {
            matterCurrent.color = colorWarnLow;
        }
        else
        {
            matterCurrent.color = colorWarnNormal;
        }
    }
    void MatterMaxed(bool isMax)
    {
        if (isMax)
        {
            matterCurrent.color = colorWarnMax;
        }
        else
        {
            matterCurrent.color = colorWarnNormal;
        }
    }
    void EnergyLow(bool isLow)
    {
        if (isLow)
        {
            energyCurrent.color = colorWarnLow;
        }
        else
        {
            energyCurrent.color = colorWarnNormal;
        }
    }
    void EnergyMaxed(bool isMax)
    {
        if (isMax)
        {
            energyCurrent.color = colorWarnMax;
        }
        else
        {
            energyCurrent.color = colorWarnNormal;
        }
    }
}
