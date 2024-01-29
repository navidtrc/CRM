import { React, useEffect, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  Tabs,
  Tab,
  FormControlLabel,
  IconButton,
  FormControl,
  OutlinedInput,
  InputAdornment,
  InputLabel,
  Radio,
  RadioGroup,
  FormLabel,
  Select,
  Grid,
  MenuItem,
  Autocomplete,
} from "@mui/material/";
import Typography from "@mui/material/Typography";
import dayjs from "dayjs";
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";
import PropTypes from "prop-types";
import { AdapterDateFnsJalali } from "@mui/x-date-pickers/AdapterDateFnsJalali";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import SendIcon from "@mui/icons-material/Send";
import CheckIcon from "@mui/icons-material/Check";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import TicketService from "../../../../services/ticket.service";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  minWidth: 750,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

function CustomTabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

CustomTabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.number.isRequired,
  value: PropTypes.number.isRequired,
};

function a11yProps(index) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

export default function AddEditTicketModal({
  open,
  onClose,
  onOpenModal,
  data = {
    ticketId: 0,
    ticketNumber: "",
    ticketDate: new Date(),
    customerId: 0,
    customerName: "",
    customerPhone: "",
    customerEmail: "",
    customerPhoneConfirmation: false,
    customerEmailConfirmation: false,
    deviceId: 0,
    deviceTypes: [],
    deviceBrand: [],
    selectedDeviceType: { id: 0, label: "" },
    selectedDeviceBrand: { id: 0, label: "" },
    deviceModel: "",
    deviceDescrption: "",
    deviceAccessories: "",
    deviceWaranty: false,
    inquiryPrice: 0,
  },
}) {
  const [tabIndex, setTabIndex] = useState(1);
  const [ticket, setTicket] = useState(data);
  const [deviceTypeSuggestion, setDeviceTypeSuggestion] = useState();
  const [deviceBrandSuggestion, setDeviceBrandSuggestion] = useState();

  // get deviceType and deviceBrand Suggestion list
  useEffect(() => {
    TicketService.prerequisite().then((result) => {
      debugger;
      setTicket((prev) => ({
        ...prev,
        ticketNumber: result.data.Data.Data.LastTicketNumber,
      }));
      setDeviceTypeSuggestion(result.data.Data.Data.DeviceTypeList);
      setDeviceBrandSuggestion(result.data.Data.Data.DeviceBrandList);
    });

    // setDeviceTypeSuggestion([
    //   { id: 1, label: "ریش تراش" },
    //   { id: 2, label: "اپیلیدی" },
    //   { id: 3, label: "تریمر" },
    //   { id: 4, label: "سشوار" },
    // ]);
    // setDeviceBrandSuggestion([
    //   { id: 1, label: "Braun" },
    //   { id: 2, label: "Philips" },
    //   { id: 3, label: "Panasonic" },
    //   { id: 4, label: "Remington" },
    // ]);
  }, []);

  const handleSubmit = () => {
    let myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const raw = JSON.stringify({
      TicketId: ticket.ticketId,
      TicketNumber: ticket.ticketNumber,
      TicketDate: ticket.ticketDate,
      CustomerName: ticket.customerName,
      CustomerPhone: ticket.customerPhone,
      CustomerEmail: ticket.customerEmail,
      // CustomerPhoneConfirmation: customerPhoneConfirmation,
      // CustomerEmailConfirmation: customerEmailConfirmation,
      DeviceTypeId: ticket.selectedDeviceType.id,
      DeviceBrandId: ticket.selectedDeviceBrand.id,
      DeviceModel: ticket.deviceModel,
      DeviceDescrption: ticket.deviceDescrption,
      DeviceAccessories: ticket.deviceAccessories,
      DeviceWaranty: ticket.deviceWaranty,
      InquiryPrice: ticket.inquiryPrice,
    });

    const requestOptions = {
      method: ticket.ticketId === 0 ? "POST" : "PUT",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    const url = ticket.ticketId === 0 ? "/api/ticket/post" : "/api/ticket/put";
    fetch(url, requestOptions)
      .then((response) => response.json())
      .then((result) => {
        if (result.IsSuccess) {
          onClose();
          Swal.fire({
            title: "انجام شد",
            icon: "success",
          });
        } else {
          onClose();
          Swal.fire({
            icon: "error",
            title: "خطا",
            text: result.Message,
          });
        }
      })
      .catch((error) => {
        Swal.fire({
          icon: "error",
          title: "خطا",
          text: error.Message,
        });
        console.log("error", error);
      });
  };

  const handleInputChange = (e) => {
    setTicket((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
    console.log(ticket);
  };

  const handleTabIndexChange = (event, newValue) => {
    setTabIndex(newValue);
  };

  return (
    <div>
      <Modal
        open={open}
        onClose={onClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style} component="form" autoComplete="off">
          <Typography id="modal-modal-title" variant="h6" component="h2">
            تیکت جدید
          </Typography>
          <Divider />

          <Box sx={{ borderBottom: 1, borderColor: "divider" }}>
            <Tabs
              value={tabIndex}
              onChange={handleTabIndexChange}
              aria-label="basic tabs example"
            >
              <Tab label="تیکت" {...a11yProps(0)} />
              <Tab label="مشتری" {...a11yProps(1)} />
              <Tab label="دستگاه" {...a11yProps(2)} />
            </Tabs>
          </Box>
          <CustomTabPanel value={tabIndex} index={0}>
            <TextField
              value={ticket.ticketNumber}
              onChange={handleInputChange}
              name="ticketNumber"
              label="شماره تیکت"
              sx={{ m: 1, width: "25ch" }}
            />
            <LocalizationProvider dateAdapter={AdapterDateFnsJalali}>
              <DateTimePicker
                label="تاریخ تیکت"
                value={ticket.ticketDate}
                onChange={(newValue) => {
                  setTicket((prev) => ({
                    ...prev,
                    ticketDate: newValue,
                  }));
                }}
                name="ticketDate"
              />
            </LocalizationProvider>
          </CustomTabPanel>
          <CustomTabPanel value={tabIndex} index={1}>
            <TextField
              value={ticket.customerName}
              onChange={handleInputChange}
              name="customerName"
              required
              label="نام و نام خانوادگی"
              variant="outlined"
            />

            <FormControl sx={{ m: 1, width: "25ch" }} variant="outlined">
              <InputLabel>شماره تماس</InputLabel>
              <OutlinedInput
                value={ticket.customerPhone}
                onChange={handleInputChange}
                name="customerPhone"
                type="text"
                endAdornment={
                  <InputAdornment position="end">
                    <IconButton
                      onClick={() =>
                        onOpenModal("phoneconfirm", ticket.customerId)
                      }
                      //onMouseDown={handleMouseDownPassword}
                      edge="end"
                    >
                      <SendIcon />
                    </IconButton>
                  </InputAdornment>
                }
                label="شماره همراه"
              />
            </FormControl>
            <FormControl sx={{ m: 1, width: "25ch" }} variant="outlined">
              <InputLabel>ایمیل</InputLabel>
              <OutlinedInput
                value={ticket.customerEmail}
                onChange={handleInputChange}
                name="customerEmail"
                type="text"
                endAdornment={
                  <InputAdornment position="end">
                    <IconButton
                      onClick={() =>
                        onOpenModal("emailconfirm", ticket.customerId)
                      }
                      //onMouseDown={handleMouseDownPassword}
                      edge="end"
                    >
                      <SendIcon />
                    </IconButton>
                  </InputAdornment>
                }
                label="ایمیل"
              />
            </FormControl>
          </CustomTabPanel>
          <CustomTabPanel value={tabIndex} index={2}>
            <div>
              <div>
                <FormControl sx={{ m: 1, minWidth: 120 }}>
                  <Autocomplete
                    value={ticket.selectedDeviceType?.label}
                    onChange={(event, newValue) => {
                      setTicket((prev) => ({
                        ...prev,
                        selectedDeviceType: newValue,
                      }));
                    }}
                    sx={{ width: 300 }}
                    renderInput={(params) => (
                      <TextField {...params} label="نوع دستگاه" />
                    )}
                    disablePortal
                    options={deviceTypeSuggestion}
                  />
                </FormControl>
                <FormControl sx={{ m: 1, minWidth: 120 }}>
                  <Autocomplete
                    value={ticket.selectedDeviceBrand?.label}
                    onChange={(event, newValue) => {
                      setTicket((prev) => ({
                        ...prev,
                        selectedDeviceBrand: newValue,
                      }));
                    }}
                    sx={{ width: 300 }}
                    renderInput={(params) => (
                      <TextField {...params} label="برند دستگاه" />
                    )}
                    disablePortal
                    options={deviceBrandSuggestion}
                  />
                </FormControl>
                <TextField
                  label="مدل دستگاه"
                  variant="standard"
                  value={ticket.deviceModel}
                  onChange={handleInputChange}
                  name="deviceModel"
                />
              </div>
              <div>
                <TextField
                  value={ticket.deviceDescrption}
                  onChange={handleInputChange}
                  name="deviceDescrption"
                  sx={{ m: 1 }}
                  placeholder="توضیحات"
                  multiline
                  rows={4}
                  maxRows={5}
                />
                <TextField
                  value={ticket.deviceAccessories}
                  onChange={handleInputChange}
                  name="deviceAccessories"
                  sx={{ m: 1 }}
                  placeholder="متعلقات"
                  multiline
                  rows={4}
                  maxRows={5}
                />
              </div>
              <div>
                <FormControl sx={{ m: 1 }}>
                  <InputLabel>برآورد هزینه</InputLabel>
                  <OutlinedInput
                    value={ticket.inquiryPrice}
                    onChange={handleInputChange}
                    name="inquiryPrice"
                    startAdornment={
                      <InputAdornment position="start">تومان</InputAdornment>
                    }
                    label="برآورد هزینه"
                  />
                </FormControl>
                <FormControl sx={{ m: 1 }}>
                  <FormLabel id="deviceWaranty">گارانتی تعمیر</FormLabel>
                  <RadioGroup
                    name="deviceWaranty"
                    onChange={handleInputChange}
                    value={ticket.deviceWaranty}
                  >
                    <FormControlLabel
                      value={false}
                      control={<Radio />}
                      label="ندارد"
                    />
                    <FormControlLabel
                      value={true}
                      control={<Radio />}
                      label="دارد"
                    />
                  </RadioGroup>
                </FormControl>
              </div>
            </div>
          </CustomTabPanel>
          <Divider />

          <Stack mt={2} spacing={2} direction="row">
            <Button
              onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              ثبت
            </Button>
            <Button onClick={() => onClose()} variant="outlined" color="error">
              انصراف
            </Button>
          </Stack>
        </Box>
      </Modal>
    </div>
  );
}
