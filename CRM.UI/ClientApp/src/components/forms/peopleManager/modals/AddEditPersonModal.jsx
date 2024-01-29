import { React, useState } from "react";
import {
  Box,
  TextField,
  Divider,
  Stack,
  Button,
  FormControlLabel,
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
  // minWidth: 900,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
  mb: 1,
  "& .MuiTextField-root": { m: 1 },
};

export default function AddEditPersonModal({
  open,
  onClose,
  data = {
    id: 0,
    name: "",
    username: "",
    email: "",
    phoneNumber: "",
  },
}) {
  const [user, setUser] = useState({
    ...data,
    password: "",
    confirmPassword: "",
  });

  const [hasUserAccess, setHasUserAccess] = useState(false);

  const handleInputChange = (e) => {
    setUser((prev) => ({
      ...prev,
      [e.target.name]: e.target.value,
    }));
  };

  const handleSubmit = () => {
    let myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const raw = JSON.stringify({
      User: {
        Id: 0,
        UserName: user.username,
        PhoneNumber: user.phoneNumber,
        Password: user.id === 0 ? user.password : "1",
        ConfirmPassword: user.id === 0 ? user.confirmPassword : "1",
        Email: user.email,
      },
      Person: {
        Id: user.id,
        ePersonType: 0,
        Name: user.name,
      },
    });

    const requestOptions = {
      method: user.id === 0 ? "POST" : "PUT",
      headers: myHeaders,
      body: raw,
      redirect: "follow",
    };

    const url = user.id === 0 ? "/api/people/post" : "/api/people/put";
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
            {user.id === 0 ? "ایجاد کاربر جدید" : "ویرایش کاربر"}
          </Typography>
          <Divider />

          <div>
            <TextField
              value={user.name}
              onChange={handleInputChange}
              name="name"
              required
              label="نام و نام خانوادگی"
              variant="standard"
            />
            <TextField
              value={user.phoneNumber}
              onChange={handleInputChange}
              name="phoneNumber"
              required
              label="شماره تماس"
              variant="standard"
            />
            <TextField
              value={user.email}
              onChange={handleInputChange}
              name="email"
              required
              label="ایمیل"
              variant="standard"
            />

            {user.id === 0 && (
              <>
                <div>
                  <FormControlLabel
                    control={
                      <Checkbox
                        name="hasUserAccess"
                        onChange={() => {
                          setHasUserAccess((prev) => {
                            return !prev;
                          });
                        }}
                        checked={hasUserAccess}
                      />
                    }
                    label="دسترسی ورود به سیستم"
                  />
                </div>
                {hasUserAccess && (
                  <>
                    <TextField
                      value={user.password}
                      onChange={handleInputChange}
                      required
                      name="password"
                      label="کلمه عبور"
                      type="password"
                      variant="standard"
                    />
                    <TextField
                      value={user.confirmPassword}
                      onChange={handleInputChange}
                      required
                      name="confirmPassword"
                      label="تکرار کلمه عبور"
                      type="password"
                      variant="standard"
                    />
                  </>
                )}
              </>
            )}
          </div>
          <Divider />

          <Typography
            color="error"
            id="modal-modal-title"
            variant="h6"
            component="h6"
          >
            {user.id === 0
              ? "شماره تماس و ایمیل را بعد از ثبت کاربر تایید کنید. در غیر این صورت پیام ارسال نمیشود"
              : "در صورت ویرایش شماره تماس یا ایمیل تایید مجدد نیاز میباشد"}
          </Typography>
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
