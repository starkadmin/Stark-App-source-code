using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace Stark
{
    public class DateHandler : MonoBehaviour
    {
        public string DateOfBirth;
        [SerializeField]private TMP_Dropdown m_day;
        [SerializeField]private TMP_Dropdown m_month;
        [SerializeField]private TMP_Dropdown m_year;

        void OnEnable()
        {
            SetupDropdownDate();
            m_year.options.Reverse();
        }
        public void Start(){
           
        }

        public void UpdateSelectedDate(){
            var monthIndex = m_month.value;
            var dayIndex = m_day.value;
            var yearIndex = m_year.value;
            DateOfBirth =$"{m_month.options[monthIndex].text}/{m_day.options[dayIndex].text}/{m_year.options[yearIndex].text}" ;
        }
        public void SetupDropdownDate(){
            StartCoroutine(SetYear());
            StartCoroutine(SetMonth());
            StartCoroutine(SetDays());
        }
        public IEnumerator SetYear(){
            int startYear = 1950;
            int currentYear = DateTime.Now.Year;
            m_year.ClearOptions();
             for (int year = startYear; year <= currentYear; year++)
            {
                TMP_Dropdown.OptionData newOptionData = new TMP_Dropdown.OptionData(year.ToString("00"));
                m_year.options.Add(newOptionData);
                m_year.RefreshShownValue();
                yield return null;
            }
            m_year.captionText.text ="YYYY";
        }
        public IEnumerator SetMonth(){
             for (int month = 1; month <= 12; month++)
            {
                TMP_Dropdown.OptionData newOptionData = new TMP_Dropdown.OptionData(month.ToString("00"));
                m_month.options.Add(newOptionData);
                m_month.RefreshShownValue();
                yield return null;
            }
            m_month.captionText.text ="MM";
        }

        public IEnumerator SetDays(){
            for (int day = 1; day <= 31; day++)
            {
                TMP_Dropdown.OptionData newOptionData = new TMP_Dropdown.OptionData(day.ToString("00"));
                m_day.options.Add(newOptionData);
                m_day.RefreshShownValue();
                yield return null;
            }
            m_day.captionText.text ="DD";
        }
    }
}
