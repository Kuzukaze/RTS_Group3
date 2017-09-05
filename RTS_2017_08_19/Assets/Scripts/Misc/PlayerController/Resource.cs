using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    private int amount;
    private int max;
    private ResourceChangeHandler changeHandler;
    private ResourceChangeHandler changeMaxHandler;
    private ResourceWarnHandler maxHandler;
    private ResourceWarnHandler lowHandler;
    private bool isInvoker = false;

    private float lowWarnCoefficient = 0.1f;
    private float maxWarnCoefficient = 0.9f;

    public int Amount
    {
        get
        {
            return amount;
        }
    }
    public int Max
    {
        get
        {
            return max;
        }
    }

    public bool Use(int value)
    {
        if (value > 0)
        {
            if (amount - value >= 0)
            {
                amount -= value;
                if(isInvoker)
                {
                    changeHandler(amount);
                    CheckWarnLimits();
                }

                return true;
            }
            else
            {
                Debug.Log("A lack of resources. Not enough matter");
                return false;
            }
        }
        else
        {
            Debug.LogError("Ignored. Resource.Use() resource value cannot be <= 0. Use Resource.Add() instead.");
            return false;
        }
    }
    public bool Add(int value)
    {
        if (value > 0)
        {
            int addingValue = value;
            if (amount + value >= max)
            {
                Debug.Log("Resource maxed.");
                addingValue = max - amount;
            }
            amount += addingValue;
            if (isInvoker)
            {
                changeHandler(amount);
                CheckWarnLimits();
            }

            return true;
        }
        else
        {
            Debug.LogError("Ignored. Resource.Add() value cannot be <= 0. Use Resource.Use() instead.");
            return false;
        }
    }
    public bool MaxDecrease(int value)
    {
        if (value > 0)
        {
            if (max - value >= 0)
            {
                max -= value;
                if (isInvoker)
                {
                    changeMaxHandler(max);
                }
                if (amount >= max)
                {
                    amount = max;
                    if (isInvoker)
                    {
                        changeHandler(amount);
                    }
                }
                if (isInvoker)
                {
                    CheckWarnLimits();
                }

                return true;
            }
            else
            {
                Debug.Log("Max resource value cannot be < 0!");
                return false;
            }
        }
        else
        {
            Debug.LogError("Ignored. Resource.MaxDecrease() value cannot be <= 0. Use Resource.MaxIncrease() instead.");
            return false;
        }
    }
    public bool MaxIncrease(int value)
    {
        if (value >= 0)
        {
            if (max == amount)
            {
                if (isInvoker)
                {
                    maxHandler(false);
                }
            }
            max += value;
            if (isInvoker)
            {
                changeMaxHandler(max);
                CheckWarnLimits();
            }

            return true;
        }
        else
        {
            Debug.LogError("Ignored. Resource.MaxIncrease() value cannot be <= 0. Use Resource.MaxDecrease() instead.");
            return false;
        }
    }

    private void CheckWarnLimits()
    {
        float currentCoefficient = (float)amount / (float)max;
        if (currentCoefficient < lowWarnCoefficient)
        {
            maxHandler(false);
            lowHandler(true);
        }
        else if (currentCoefficient < maxWarnCoefficient)
        {
            lowHandler(false);
            maxHandler(false);
        }
        else
        {
            lowHandler(false);
            maxHandler(true);
        }
    }

    public void SetIsInvoker(bool value)
    {
        isInvoker = value;
    }

    public Resource (int amount, int max, ResourceChangeHandler changeHandler, ResourceChangeHandler changeMaxHandler, ResourceWarnHandler lowHandler, ResourceWarnHandler maxHandler, float lowWarnCoefficient, float maxWarnCoefficient)
    {
        this.amount =               amount;
        this.max =                  max;
        this.changeHandler =        changeHandler;
        this.changeMaxHandler =     changeMaxHandler;
        this.lowHandler =           lowHandler;
        this.maxHandler =           maxHandler;
        this.lowWarnCoefficient =   lowWarnCoefficient;
        this.maxWarnCoefficient =   maxWarnCoefficient;
    }

    public Resource(Resource baseResource)
    {
        this.amount =               baseResource.amount;
        this.max =                  baseResource.max;
        this.changeHandler =        baseResource.changeHandler;
        this.changeMaxHandler =     baseResource.changeMaxHandler;
        this.lowHandler =           baseResource.lowHandler;
        this.maxHandler =           baseResource.maxHandler;
        this.lowWarnCoefficient =   baseResource.lowWarnCoefficient;
        this.maxWarnCoefficient =   baseResource.maxWarnCoefficient;
    }

    public void Init(int amount, int max, ResourceChangeHandler changeHandler, ResourceChangeHandler changeMaxHandler, ResourceWarnHandler lowHandler, ResourceWarnHandler maxHandler, float lowWarnCoefficient, float maxWarnCoefficient)
    {
        this.amount =               amount;
        this.max =                  max;
        this.changeHandler =        changeHandler;
        this.changeMaxHandler =     changeMaxHandler;
        this.lowHandler =           lowHandler;
        this.maxHandler =           maxHandler;
        this.lowWarnCoefficient =   lowWarnCoefficient;
        this.maxWarnCoefficient =   maxWarnCoefficient;
    }
}
