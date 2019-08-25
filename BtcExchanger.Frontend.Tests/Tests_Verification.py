from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from Tests_Transaction import complete_with_correct_data
from runpy import run_path
settings = run_path("../.env")
site_url = settings['FRONTEND_SITE_URL']+":"+str(settings['FRONTEND_SITE_PORT'])
driver = settings['DRIVER']

def error_verification_code(browser):
    browser.get(site_url)
    getToVerificationSite(browser)
    #check twice if there is some lags
    try:
        verification_code = browser.find_element_by_id('verification-code')
    except:
        verification_code = browser.find_element_by_id('verification-code')
    verification_code.send_keys('4321')
    verification_code.send_keys(Keys.ENTER)
    #check twice if there is some lags
    try:
        verification_code_error = browser.find_element_by_id('verification-error')
    except:
        verification_code_error = browser.find_element_by_id('verification-error')
    assert "Your verification code is invalid." in verification_code_error.text

def all_corect(browser):
    browser.get(site_url)
    getToVerificationSite(browser)
    #check twice if there is some lags
    try:
        verification_code = browser.find_element_by_id('verification-code')
    except:
        verification_code = browser.find_element_by_id('verification-code')
    verification_code.send_keys('1234')
    verification_code.send_keys(Keys.ENTER)
    #check twice if there is some lags
    try:
        final_status = browser.find_element_by_id('status')
    except:
        final_status = browser.find_element_by_id('status')
    assert "Waiting for money transfer." in final_status.text

def getToVerificationSite(browser):
    complete_with_correct_data(browser)
    browser.find_element_by_id('btc-quantity').send_keys(Keys.ENTER)    

if __name__ == "__main__":
    browser = webdriver.Chrome(driver)
    error_verification_code(browser)
    all_corect(browser)
    browser.close()