using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour {

    public Text valueText;
    public Image barImage;
    public bool hideIfMinValue;

    private float _currVal;
    private float _maxVal;
    private Parameter _attachedParam;

    public void Init(float currVal, float maxVal = 0)
    {
        _maxVal = maxVal;
        _currVal = currVal;
        UpdateView();
    }

    public void AttachParameter(IParamOwner paramOwner, string paramName)
    {
        if (paramOwner.parameters.ContainsKey(paramName))
        {
            AttachParameter(paramOwner.parameters[paramName]);
        }
        else
        {
            Hide();
            paramOwner.onParamAttached += WaitParam;
            paramOwner.onParamDetached += ReleaseParam;
        }
    }

    private void ReleaseParam(Parameter param)
    {
        Hide();
    }

    private void WaitParam(Parameter param)
    {
        Show();
        AttachParameter(param);
    }

    public void AttachParameter(Parameter param)
    {
        _attachedParam = param;
        _attachedParam.onChange += SetValue;
        SetValue(_attachedParam.value);
    }

    public void SetValue(float oldVal, float newVal)
    {
        SetValue(newVal);
    }

    public void SetValue(float val)
    {
        _currVal = val;
        UpdateView();
    }

    private void UpdateView()
    {
        //Check if need to hide indicator
        bool hide = (hideIfMinValue && _attachedParam != null && _currVal <= _attachedParam.minValue);
        if (hide) { Hide(); } else { Show(); }

        //Update text.
        if (valueText != null)
        {
            valueText.text = _maxVal != 0f ?
                string.Format("{0}/{1}", _currVal.ToString(), _maxVal.ToString()) :
                _currVal.ToString();
        }

        //Update sprite fill.
        if(barImage != null)
        {
            barImage.fillAmount = _currVal / _maxVal;
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    internal void Release()
    {
        SetValue(0);
        _attachedParam = null;
    }
}
