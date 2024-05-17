package main

import (
	"bytes"
	"encoding/xml"
	"fmt"
	"io"
	"io/ioutil"
	"os"
	"path/filepath"
	"strconv"
	"strings"

	"github.com/360EntSecGroup-Skylar/excelize"
	"golang.org/x/text/encoding/simplifiedchinese"
	"golang.org/x/text/transform"
)

func XmlToExcel(fileName string, filepathValue string) {
	// 打开GBK编码的XML文件
	xmlFile := filepath.Join(filepathValue, fileName)
	file, err := os.Open(xmlFile)
	if err != nil {
		fmt.Println(err)
		return
	}
	defer file.Close()

	data, err := io.ReadAll(file)
	if err != nil {
		fmt.Println(err)
		return
	}

	// 创建GBK编码转换器
	gbkDecoder := simplifiedchinese.GBK.NewDecoder()

	// 使用转换器包装文件
	reader := transform.NewReader(bytes.NewReader(data), gbkDecoder)
	bytesData, err := ioutil.ReadAll(reader)
	if err != nil {
		fmt.Println(err)
		return
	}
	str := string(bytesData)
	str = strings.ReplaceAll(str, `<?xml version="1.0" encoding="gbk" ?>`, `<?xml version="1.0" encoding="UTF-8" ?>`)
	str = strings.ReplaceAll(str, `<?xml version="1.0" encoding="GBK" ?>`, `<?xml version="1.0" encoding="UTF-8" ?>`)
	str = strings.ReplaceAll(str, `<?xml version="1.0" encoding="gb2312" ?>`, `<?xml version="1.0" encoding="UTF-8" ?>`)
	str = strings.ReplaceAll(str, `<?xml version="1.0" encoding="GB2312" ?>`, `<?xml version="1.0" encoding="UTF-8" ?>`)
	str = strings.ReplaceAll(str, `<?xml version="1.0" encoding="GB2312"?>`, `<?xml version="1.0" encoding="UTF-8" ?>`)
	// fmt.Println(str)
	rows, err := parseXML([]byte(str))
	if err != nil {
		fmt.Println(err)
		return
	}
	if len(rows) <= 0 {
		fmt.Println("空数据")
		return
	}

	f := excelize.NewFile()
	// Create a new sheet.
	indexNum := f.NewSheet("数据")
	// Set value of a cell.

	f.SetCellValue("数据", "A1", "作废日期")
	f.SetCellValue("数据", "B1", "原发票号码")
	f.SetCellValue("数据", "C1", "税额")
	f.SetCellValue("数据", "D1", "票信息表编号")
	f.SetCellValue("数据", "E1", "原发票代码")
	f.SetCellValue("数据", "F1", "价税合计")
	f.SetCellValue("数据", "G1", "客户识别号")
	f.SetCellValue("数据", "H1", "客户名称")
	f.SetCellValue("数据", "I1", "发票类型")
	f.SetCellValue("数据", "J1", "主要商品名称")
	f.SetCellValue("数据", "K1", "上传状态")
	f.SetCellValue("数据", "L1", "合计金额")
	f.SetCellValue("数据", "M1", "清单标识")
	f.SetCellValue("数据", "N1", "发票号码")
	f.SetCellValue("数据", "O1", "开票日期")
	f.SetCellValue("数据", "P1", "发票状态")
	f.SetCellValue("数据", "Q1", "作废人")
	f.SetCellValue("数据", "R1", "开票人")
	f.SetCellValue("数据", "S1", "发票代码")

	//开始写入数据
	index := 2
	for _, r := range rows {
		f.SetCellValue("数据", "A"+strconv.Itoa(index), r.DepDate)
		f.SetCellValue("数据", "B"+strconv.Itoa(index), r.OrignNo)
		f.SetCellValue("数据", "C"+strconv.Itoa(index), r.Rate)
		f.SetCellValue("数据", "D"+strconv.Itoa(index), r.PiaoNo)
		f.SetCellValue("数据", "E"+strconv.Itoa(index), r.OrignFaNo)
		f.SetCellValue("数据", "F"+strconv.Itoa(index), r.PriceTotal)
		f.SetCellValue("数据", "G"+strconv.Itoa(index), r.CustNo)
		f.SetCellValue("数据", "H"+strconv.Itoa(index), r.CustomerName)
		f.SetCellValue("数据", "I"+strconv.Itoa(index), r.FaType)
		f.SetCellValue("数据", "J"+strconv.Itoa(index), r.MainGoods)
		f.SetCellValue("数据", "K"+strconv.Itoa(index), r.UploadStatus)
		f.SetCellValue("数据", "L"+strconv.Itoa(index), r.CalCash)
		f.SetCellValue("数据", "M"+strconv.Itoa(index), r.ListNo)
		f.SetCellValue("数据", "N"+strconv.Itoa(index), r.FaNum)
		f.SetCellValue("数据", "O"+strconv.Itoa(index), r.FaDate)
		f.SetCellValue("数据", "P"+strconv.Itoa(index), r.FaTai)
		f.SetCellValue("数据", "Q"+strconv.Itoa(index), r.FerRen)
		f.SetCellValue("数据", "R"+strconv.Itoa(index), r.FaRen)
		f.SetCellValue("数据", "S"+strconv.Itoa(index), r.FaNo)
		index++
	}
	index = 0

	// Set active sheet of the workbook.
	f.SetActiveSheet(indexNum)
	// Save spreadsheet by the given path.
	excelName := strings.ReplaceAll(fileName, ".xml", ".xlsx")
	fullName := filepath.Join(filepathValue, excelName)
	if err := f.SaveAs(fullName); err != nil {
		fmt.Println(err)
	}

}

type Data struct {
	XMLName xml.Name `xml:"Data"`
	Ykfp    []Ykfp   `xml:"YKFP"`
}

type Ykfp struct {
	XMLName xml.Name `xml:"YKFP"`
	Records []Row    `xml:"Row"`
}

type Row struct {
	XMLName      xml.Name `xml:"Row"`
	DepDate      string   `xml:"作废日期,attr"`
	OrignNo      string   `xml:"原发票号码,attr"`
	Rate         float64  `xml:"税额,attr"`
	PiaoNo       string   `xml:"票信息表编号,attr"`
	OrignFaNo    string   `xml:"原发票代码,attr"`
	PriceTotal   float64  `xml:"价税合计,attr"`
	CustNo       string   `xml:"客户识别号,attr"`
	CustomerName string   `xml:"客户名称,attr"`
	FaType       string   `xml:"发票类型,attr"`
	MainGoods    string   `xml:"主要商品名称,attr"`
	UploadStatus string   `xml:"上传状态,attr"`
	CalCash      float64  `xml:"合计金额,attr"`
	ListNo       string   `xml:"清单标识,attr"`
	FaNum        string   `xml:"发票号码,attr"`
	FaDate       string   `xml:"开票日期,attr"`
	FaTai        string   `xml:"发票状态,attr"`
	FerRen       string   `xml:"作废人,attr"`
	FaRen        string   `xml:"开票人,attr"`
	FaNo         string   `xml:"发票代码,attr"`
}

func parseXML(data []byte) ([]Row, error) {
	var content Data // Assuming a suitable struct or type to unmarshal the XML into
	err := xml.Unmarshal(data, &content)
	if err != nil {
		fmt.Println(err)
		return nil, err
	}
	rows := []Row{}
	for _, row := range content.Ykfp { // Placeholder for actual operations on the unmarshaled data
		for _, col := range row.Records {
			fmt.Println(col)
			rows = append(rows, Row(col))
		}
	}
	return rows, nil
}

func GetLimit() {
	// 打开文件
	file, err := excelize.OpenFile("/Users/loern/Desktop/Result_37.xlsx")
	if err != nil {
		fmt.Println(err)
		return
	}

	fmt.Println(file.Path)

	sheetName := file.GetSheetName(1)

	// 读取数据中的内容
	Rows := file.GetRows(sheetName)

	fmt.Println(len(Rows))
	// 遍历所有行
	for _, row := range Rows {
		// 遍历每行的单元格
		// fmt.Println(row)
		fmt.Printf("INSERT INTO foodismx_talent.star_visitingschedule_servicefeee_limit ( visiting_schedule_id, goods_id, host_id, create_time) VALUES ( %s, %s, %s, now());", row[2], row[3], row[4])
		fmt.Println()
	}
}

func main() {
	XmlToExcel("已开发票增值税发票导出_20240506094042.xml", "/Users/loern/Desktop/")
	// XmlToExcel("example.xml", "C:\\Users\\Administrator\\Desktop\\")
	// GetLimit()
}
