import { React, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  IconButton,
  FormControl,
  Paper,
  OutlinedInput,
  FormControlLabel,
  InputAdornment,
  InputLabel,
  Radio,
  RadioGroup,
  FormLabel,
  Select,
  MenuItem,
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

export default function WaitingStateModal({
  open,
  onClose,
  data = {
    ticketId: 0,
    number: "",
    date: "",
    customerId: 0,
    firstName: "",
    lastName: "",
    phone: "",
    email: "",
    phoneConfirmation: false,
    emailConfirmation: false,
    deviceId: 0,
    deviceType: {},
    deviceBrand: {},
    model: "",
    descrption: "",
    accessories: "",
    waranty: false,
    inquiryPrice: "",
  },
}) {
  // const handleChange = (event) => {
  //   setAge(event.target.value);
  // };

  // return (
  //   <div>
  //     <Modal
  //       // style={{ minWidth: "800px" }}
  //       open={open}
  //       onClose={onClose}
  //       aria-labelledby="modal-modal-title"
  //       aria-describedby="modal-modal-description"
  //     >
  //       <Box sx={style} component="form" autoComplete="off">
  //         <Typography id="modal-modal-title" variant="h6" component="h2">
  //           ارسال تیکت به مرکز تعمیر
  //         </Typography>
  //         <Divider />

  //         <TextField
  //           disabled
  //           label="شماره تیکت"
  //           id="ticketNumberInput"
  //           sx={{ m: 1, width: "25ch" }}
  //         />

  //         <TextField label="تاریخ تیکت" disabled defaultValue={new Date()} />
  //         <TextField
  //           disabled
  //           name="firstName"
  //           required
  //           label="نام"
  //           variant="standard"
  //         />
  //         <TextField
  //           disabled
  //           name="lastName"
  //           required
  //           label="نام خانوادگی"
  //           variant="standard"
  //         />
  //         <TextField
  //           disabled
  //           name="phoneNumber"
  //           required
  //           label="شماره تماس"
  //           variant="standard"
  //         />
  //         <TextField
  //           disabled
  //           name="email"
  //           required
  //           label="ایمیل"
  //           variant="standard"
  //         />

  //         <div>
  //           <div>
  //             <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
  //               <TextField variant="standard" disabled>
  //                 نوع دستکاه
  //               </TextField>
  //             </FormControl>
  //             <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
  //               <TextField variant="standard" disabled>
  //                 برند دستکاه
  //               </TextField>
  //             </FormControl>
  //             <TextField disabled label="مدل دستگاه" variant="standard" />
  //           </div>
  //           <div>
  //             <TextField
  //               disabled
  //               sx={{ m: 1 }}
  //               placeholder="توضیحات"
  //               multiline
  //               rows={4}
  //               maxRows={5}
  //             />
  //             <TextField
  //               disabled
  //               sx={{ m: 1 }}
  //               placeholder="متعلقات"
  //               multiline
  //               rows={4}
  //               maxRows={5}
  //             />
  //           </div>
  //           <div>
  //             <FormControl sx={{ m: 1 }}>
  //               <InputLabel>برآورد هزینه</InputLabel>
  //               <OutlinedInput
  //                 disabled
  //                 startAdornment={
  //                   <InputAdornment position="start">تومان</InputAdornment>
  //                 }
  //                 label="برآورد هزینه"
  //               />
  //             </FormControl>
  //             <FormControl sx={{ m: 1 }}>
  //             <TextField variant="standard" disabled>
  //                 گارانتی تعمیر ندارد
  //               </TextField>
  //             </FormControl>
  //           </div>
  //           <div></div>
  //         </div>
  //         <Divider />

  //         <Stack mt={2} spacing={2} direction="row">
  //           <Button
  //             // onClick={() => handleSubmit()}
  //             variant="contained"
  //             color="success"
  //           >
  //             ثبت
  //           </Button>
  //           <Button onClick={() => onClose()} variant="outlined" color="error">
  //             انصراف
  //           </Button>
  //         </Stack>
  //       </Box>
  //     </Modal>
  //   </div>
  // );

  const ticketNumber = "123456";
  const ticketDate = "2024-01-27";
  const customerName = "Ali Reza";
  const customerPhone = "09123456789";
  const customerEmail = "ali.reza@example.com";
  const deviceType = "Laptop";
  const deviceBrand = "Lenovo";
  const deviceModel = "ThinkPad T14";
  const deviceProblem = "Broken screen";
  const deviceAccessories = "Charger, mouse, keyboard";
  const devicePrice = "5,000,000 تومان";
  const deviceWarranty = true;

  return (
    <Modal
      // style={{ minWidth: "800px" }}
      open={open}
      onClose={onClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Paper elevation={3} sx={{ p: 2 }}>
          <Typography id="modal-modal-title" variant="h5" component="h5">
            ارسال تیکت به مرکز تعمیر
          </Typography>
          <Divider />
          <Box sx={{ m: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">شماره تیکت: {ticketNumber}</Typography>
            <Typography variant="h6">تاریخ تیکت: {ticketDate}</Typography>
          </Box>
          <Divider />
          
          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">نام مشتری: {customerName}</Typography>
            <Typography color="error" variant="h6">شماره تماس: {customerPhone}</Typography>
            <Typography color="success" >ایمیل: {customerEmail}</Typography>
          </Box>
          <Divider />

          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">نوع دستگاه: {deviceType}</Typography>
            <Typography variant="h6">برند دستگاه: {deviceBrand}</Typography>
            <Typography variant="h6">مدل دستگاه: {deviceModel}</Typography>
          </Box>
          <Box sx={{ mt: 2 }}>
            <Typography variant="h6">مشکل دستگاه: {deviceProblem}</Typography>
            <Typography variant="h6">
              متعلقات دستگاه: {deviceAccessories}
            </Typography>
          </Box>
          <Box sx={{ mt: 2, display: "flex", justifyContent: "space-between" }}>
            <Typography variant="h6">قیمت برآورد شده: {devicePrice}</Typography>
            <Typography variant="h6">
              گارانتی :{deviceWarranty === true ? "دارد" : "ندارد"}
            </Typography>
          </Box>
          <Divider />

          <Stack mt={2} spacing={2} direction="row">
            <Button
              // onClick={() => handleSubmit()}
              variant="contained"
              color="success"
            >
              ثبت
            </Button>
            <Button onClick={() => onClose()} variant="outlined" color="error">
              انصراف
            </Button>
          </Stack>
        </Paper>
      </Box>
    </Modal>
  );
}
