import { React, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  FormControl,
  Paper,
  Radio,
  FormControlLabel,
  RadioGroup,
  FormLabel,
} from "@mui/material/";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";
import PropTypes from "prop-types";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: "80%",
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

export default function InquiryStateModal({
  open,
  onClose,
  data = {
    ticketId: 0,
    ticketNumber: "123456",
    ticketDate: "2024-01-27",
    customerName: "Ali Reza",
    customerPhone: "09123456789",
    customerEmail: "ali.reza@example.com",
    phoneConfirmation: false,
    emailConfirmation: false,
    deviceType: "Laptop",
    deviceBrand: "Lenovo",
    deviceModel: "ThinkPad T14",
    descrption: "Broken screen",
    accessories: "Charger, mouse, keyboard",
    deviceWaranty: false,
    inquiryPrice: "5,000,000 تومان",
    repairerPrice: "4,000,000 تومان",
    totalPrice: "6,000,000 تومان",
  },
}) {
  const [ticketId, setTicketId] = useState(data.ticketId);
  const [inquiryResult, setInquiryResult] = useState(false);

  const handleInquiryResultChange = () => {
    setInquiryResult((prev) => {
      return !prev;
    });
  };

  const handleSubmit = () => {};

  return (
    <Modal
      open={open}
      onClose={onClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Paper elevation={3} sx={{ p: 2 }}>
          <Typography id="modal-modal-title" variant="h5" component="h5">
            استعلام از مشتری
          </Typography>
          <Divider />
          <Box sx={{ m: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              شماره تیکت: {data.ticketNumber}
            </Typography>
            <Typography variant="h6">تاریخ تیکت: {data.ticketDate}</Typography>
          </Box>
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">نام مشتری: {data.customerName}</Typography>
            <Typography
              color={data.phoneConfirmation ? "" : "error"}
              variant="h6"
            >
              شماره تماس: {data.customerPhone}
            </Typography>
            <Typography color={data.emailConfirmation ? "" : "error"}>
              ایمیل: {data.customerEmail}
            </Typography>
          </Box>
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">نوع دستگاه: {data.deviceType}</Typography>
            <Typography variant="h6">
              برند دستگاه: {data.deviceBrand}
            </Typography>
            <Typography variant="h6">مدل دستگاه: {data.deviceModel}</Typography>
          </Box>
          <Box sx={{ mt: 2 }}>
            <Typography variant="h6">مشکل دستگاه: {data.descrption}</Typography>
            <Typography variant="h6">
              متعلقات دستگاه: {data.accessories}
            </Typography>
          </Box>

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              قیمت برآورد شده: {data.inquiryPrice}
            </Typography>
            <Typography variant="h6">
              گارانتی :{data.deviceWaranty === true ? "دارد" : "ندارد"}
            </Typography>
          </Box>
          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">
              قیمت تعمیرکار: {data.repairerPrice}
            </Typography>
            <Typography variant="h5">قیمت نهایی: {data.totalPrice}</Typography>
          </Box>
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <FormControl sx={{ m: 1 }}>
              <FormLabel id="inquiryResult">نتیجه ی استعلام</FormLabel>
              <RadioGroup
                name="inquiryResult"
                onChange={handleInquiryResultChange}
                value={inquiryResult}
              >
                <FormControlLabel
                  value={false}
                  control={<Radio />}
                  label="منفی"
                />
                <FormControlLabel
                  value={true}
                  control={<Radio />}
                  label="مثبت"
                />
              </RadioGroup>
            </FormControl>
            <TextField
              sx={{ m: 1 }}
              placeholder="توضیحات (خصوصی)"
              multiline
              rows={4}
              maxRows={5}
            />
          </Box>

          <Stack mt={2} spacing={2} direction="row">
            <Button
              onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              {inquiryResult ? "ادامه فرآیند تعمیر" : "برگشت به مرکز فروش"}
            </Button>
            <Button onClick={() => onClose()} variant="contained" color="error">
              انصراف
            </Button>
          </Stack>
        </Paper>
      </Box>
    </Modal>
  );
}
const top100Films = [
  { label: "فرشید", id: 1 },
  { label: "تست 1", id: 2 },
  { label: "تست 2", id: 3 },
];
