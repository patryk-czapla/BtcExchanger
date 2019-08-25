from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from runpy import run_path
settings = run_path("../.env")
site_url = settings['FRONTEND_SITE_URL']+":"+str(settings['FRONTEND_SITE_PORT'])
driver = settings['DRIVER']

def error_email(browser):
    browser.get(site_url)
    complete_with_correct_data(browser)
    email = browser.find_element_by_id('email')
    email.send_keys('@')
    email.send_keys(Keys.ENTER)
    #check twice if there is some lags
    try:
        email_error = browser.find_element_by_id('email-error')
    except:
        email_error = browser.find_element_by_id('email-error')
    assert "The email field is not a valid e-mail address." in email_error.text

def error_account_number(browser):
    browser.get(site_url)
    account_number = browser.find_element_by_id('account-number')
    complete_with_correct_data(browser)
    account_number.send_keys('a')
    account_number.send_keys(Keys.ENTER)
    #check twice if there is some lags
    try:
        btc_error = browser.find_element_by_id('account_number-error')
    except:
        btc_error = browser.find_element_by_id('account_number-error')
    assert "The account_number field is not a valid credit card number." in btc_error.text

def error_phone_number(browser):
    browser.get(site_url)
    browser.find_element_by_id('contact-switch').click()
    phone_number = browser.find_element_by_id('phone-number')
    complete_with_correct_data(browser)
    phone_number.send_keys('a')
    phone_number.send_keys(Keys.ENTER)
    #check twice if there is some lags
    try:
        phone_number_error = browser.find_element_by_id('phone_number-error')
    except:
        phone_number_error = browser.find_element_by_id('phone_number-error')
    assert "The phone_number field is not a valid phone number." in phone_number_error.text

def all_corect(browser):
    browser.get(site_url)
    complete_with_correct_data(browser)
    browser.find_element_by_id('btc-quantity').send_keys(Keys.ENTER)
    #check twice if there is some lags
    try:
        verification_code = browser.find_element_by_id('verification-code')
    except:
        verification_code = browser.find_element_by_id('verification-code')
    assert "" in verification_code.text

def complete_with_correct_data(browser):
    btc_quantity = browser.find_element_by_id('btc-quantity')
    account_number = browser.find_element_by_id('account-number')
    btc_quantity.send_keys(Keys.CONTROL + "a")
    btc_quantity.send_keys(Keys.DELETE)
    btc_quantity.send_keys('1,1')
    account_number.send_keys('3566002020360505')
    try:
        contact = browser.find_element_by_id('phone-number')
        contact.send_keys('1212')
    except:
        contact = browser.find_element_by_id('email')
        contact.send_keys('batman@bat.mobile')

if __name__ == "__main__":
    browser = webdriver.Chrome(driver)
    error_account_number(browser)
    error_email(browser)
    error_phone_number(browser)
    all_corect(browser)
    browser.close()