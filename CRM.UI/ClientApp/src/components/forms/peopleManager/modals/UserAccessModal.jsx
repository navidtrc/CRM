import { React, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  FormControl,
  FormLabel,
  RadioGroup,
  FormControlLabel,
  Radio,
  Checkbox,
} from "@mui/material/";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import Swal from "sweetalert2";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

export default function UserAccessModal({ open, onClose, user }) {
  const [data, setData] = useState({
    changePassword: false,
    lockoutEnabled: user.lockoutEnabled,
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    setData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };
  const handleCheckboxChange = (e) => {
    setData((prev) => ({
      ...prev,
      [e.target.name]: e.target.checked,
    }));
  };

  const handleSubmit = () => {
    let myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const raw = JSON.stringify({
      Id: user.userId,
      LockoutEnabled: data.lockoutEnabled,
      ChangePassword: data.changePassword,
      Password: data.password,
      ConfirmPassword: data.confirmPassword,
    });

    const requestOptions = {
      method: "POST",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    fetch("/api/account/UserAccessChange", requestOptions)
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

  return (
    <div>
      <Modal
        open={open}
        onClose={onClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style} component="form" autoComplete="off">
          <Typography id="modal-modal-title" variant="h5" component="h5">
            دسترسی ورود به سیستم
          </Typography>
          <Divider />
          <Typography variant="body1" component="body1">
            کاربر: {user.name}
          </Typography>

          <div>
            <FormControl>
              <FormLabel id="userLockoutEnabled">وضعیت</FormLabel>
              <RadioGroup
                aria-labelledby="userLockoutEnabled"
                name="lockoutEnabled"
                onChange={handleChange}
                value={data.lockoutEnabled}
              >
                <FormControlLabel
                  value={false}
                  control={<Radio />}
                  label="فعال"
                />
                <FormControlLabel
                  value={true}
                  control={<Radio />}
                  label="غیرفعال"
                />
              </RadioGroup>
            </FormControl>
          </div>

          <div>
            <FormControlLabel
              control={
                <Checkbox
                  name="changePassword"
                  onChange={handleCheckboxChange}
                  checked={data.changePassword}
                />
              }
              label="آیا میخواهید کلمه عبور را تغییر دهید؟"
            />
          </div>

          {data.changePassword && (
            <>
              <TextField
                value={data.password}
                onChange={handleChange}
                required
                name="password"
                label="کلمه عبور"
                type="password"
                variant="standard"
              />
              <TextField
                value={data.confirmPassword}
                onChange={handleChange}
                required
                name="confirmPassword"
                label="تکرار کلمه عبور"
                type="password"
                variant="standard"
              />
            </>
          )}

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
