import puppeteer from 'puppeteer';
import orm from './db';
import { toDate } from './services/format';
let js: any, document: any;
const jobs = async () => {
  let monthDate;
  const bank = (await orm.qrVaucher.findFirst({
    orderBy:{
      TransactionDate:'asc'
    },
  }))

  if(bank){
    monthDate = (bank.UpdatedOn?.getMonth() || 0 ) + 1
  }
  console.log(monthDate)
  const browser = await puppeteer.launch({
    executablePath: './chrome-win64/chrome.exe',
    headless: 'new'
  });
  const page = await browser.newPage();
  await page.goto('https://marketapi.bnb.com.bo/ApiMarket/');
  await page.setViewport({width: 1080, height: 1024});
  await page.type('#user', 'MAROSALES');
  await page.type('#password', 'MAROSALES2023');
  await page.click('#loginData');

  const checkVariableLoaded = async () => {
    return await page.evaluate(() => {
      return typeof js !== 'undefined';
    });
  };

  const textSelector = await page.waitForSelector(
    'text/ROSALES JANDULA MARIA JOSE - Dashboard de Cuenta'
  );
  await textSelector?.evaluate(el => el.textContent);
  
  const variablePromise = new Promise<void>(async (resolve) => {
    let variableCargada = false;
    while (!variableCargada) {
      variableCargada = await checkVariableLoaded();
      if (variableCargada) {
        resolve();
      } else {
        await new Promise( r => setTimeout(r,100));
      }
    }
  });

  await variablePromise;

  await page.evaluate(() => {
    document.getElementById("moduleMain").style.display = "none";
  document.getElementById("moduleServices").style.display = "none";
  document.getElementById("moduleConfiguration").style.display = "none";
  document.getElementById("moduleReports").style.display = "";
  document.getElementById("moduleAccount").style.display = "none";
  });

  const tieneDatos = await page.evaluate(() => {
    const tabla = document.querySelector('table#tableReport'); // Reemplaza con tu selector de tabla
    const filas = tabla.querySelectorAll('table#tableReport > tbody > tr');
    return filas.length > 0;
  });

  const trPromise = new Promise<void>(async (resolve) => {
    let trCargada = false;
    while (!trCargada) {
      trCargada = tieneDatos
      if (trCargada) {
        resolve();
      } else {
        await new Promise( r => setTimeout(r,150));
      }
    }
  });  
  var year = new Date().getFullYear();
  var day = new Date().getDate();
  
  var maxMonth = new Date().getMonth() + 1;
  var month = monthDate || 1
  for (month; month < maxMonth; month++) {
    var previusMonth = ("0" +(month)).slice(-2);
    var nextMonth = ("0" +(month + 1)).slice(-2);

    await page.evaluate((selector, fecha) => {
      document.querySelector(selector).value = fecha;
    }, '#initDate', `${day}/${previusMonth}/${year}`);

    await page.evaluate((selector, fecha) => {
      document.querySelector(selector).value = fecha;
    },'#finalDate', `${day}/${nextMonth}/${year}`);

    await page.evaluate((selector) => {
      const datepickerElement = document.querySelector(selector);
      datepickerElement.dispatchEvent(new Event('change'));
    }, '#initDate');

    await page.evaluate((selector) => {
      const datepickerElement = document.querySelector(selector);
      datepickerElement.dispatchEvent(new Event('change'));
    }, '#finalDate');
    
    await page.click('#searchReport');
    await new Promise( r => setTimeout(r,3*1000));
    await trPromise;
    let tableSelector = 'table#tableReport'; // Reemplaza con tu selector
    let table = await page.$(tableSelector);
    let rows: any | [] = await table?.$$('tbody > tr');
    if(rows.length===10){
      await page.select('select[name="tableReport_length"]', '100')
      rows = await table?.$$('tbody > tr');
    }
    if(rows.length>10) {
      for (const row of rows) {
        let rowData = await page.evaluate(row => {
          let columns = row.querySelectorAll('td'); // Obtén las celdas de la fila
          return Array.from(columns, (column: any) => column.textContent);
        }, row);
        if (rowData.length === 13){
          try {
            const data = {
              BankId: rowData[0],
              GenerationDate: toDate(rowData[1]),
              TransactionDate: toDate(rowData[2]),
              Amount: parseFloat(rowData[3]),
              Currency: rowData[4],
              Gloss: rowData[5],
              OriginName: rowData[6],
              SourceBank: rowData[7],
              SourceAccountNumber: rowData[8],
              DestinationAccountNumber: rowData[9],
              BankingCode: rowData[10],
              Error: rowData[11],
              Status: rowData[12],
              CreatedOn: new Date(),
              UpdatedOn: new Date(),
            }
            const newQrVaucher = await orm.qrVaucher.create({data})
            console.log(newQrVaucher)
          } catch (error) {
            console.log('error')
          }
        }
      }
    }
  }
  console.log('Finalizo')
  await browser.close();
}
//cada 3 minutos
const miliSeconds = 2 * 30 * 1000;

jobs()
setInterval(jobs, miliSeconds);
