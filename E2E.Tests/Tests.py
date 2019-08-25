from selenium import webdriver
from selenium.webdriver.common.keys import Keys
from runpy import run_path
settings = run_path("../.env")
site_url = settings['FRONTEND_SITE_URL']+":"+str(settings['FRONTEND_SITE_PORT'])
driver = settings['DRIVER']

def all_corect(browser):
    browser.get(site_url)
    btc_quantity = browser.find_element_by_id('btc-quantity')
    account_number = browser.find_element_by_id('account-number')
    email = browser.find_element_by_id('email')
    btc_quantity.send_keys(Keys.CONTROL + "a")
    btc_quantity.send_keys(Keys.DELETE)
    btc_quantity.send_keys('1.1')
    account_number.send_keys('3566002020360505')
    email.send_keys('batman@bat.mobil')
    btc_quantity.send_keys(Keys.ENTER)
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

if __name__ == "__main__":
    browser = webdriver.Chrome(driver)
    all_corect(browser)
    browser.close()