import { useState } from "react";
import StaffDataGrid from "./StaffDataGrid";
import AddEditUserModal from "./modals/AddEditUser";
import ChangePasswordModal from "./modals/ChangePassword";
import EmailConfirmation from "../global/modals/EmailConfirmation";
import PhoneConfirmation from "../global/modals/PhoneConfirmation";

const StaffManager = () => {
  const [user, setUser] = useState(null);
  const [addEditOpen, setAddEditOpen] = useState(false);
  const [changePasswordOpen, setChangePasswordOpen] = useState(false);
  const [emailConfirmOpen, setEmailConfirmOpen] = useState(false);
  const [phoneConfirmOpen, setPhoneConfirmOpen] = useState(false);
  const [isRefetching, setIsRefetching] = useState(false);

  function modalOpenHandler(type, payload) {
    debugger;
    switch (type) {
      case "addedit":
        setUser(payload);
        setAddEditOpen(true);
        break;
      case "changepassword":
        setUser(payload);
        setChangePasswordOpen(true);
        break;
      case "emailconfirm":
        setUser(payload);
        setEmailConfirmOpen(true);
        break;
      case "phoneconfirm":
        setUser(payload);
        setPhoneConfirmOpen(true);
        break;
      default:
        break;
    }
  }

  return (
    <>
      {addEditOpen && (
        <AddEditUserModal
          open={addEditOpen}
          onClose={() => {
            setAddEditOpen(false);
            setUser(null);
            setIsRefetching(true);
          }}
          data={user}
        />
      )}
      {changePasswordOpen && (
        <ChangePasswordModal
          open={changePasswordOpen}
          onClose={() => setChangePasswordOpen(false)}
          user={user}
        />
      )}

      {emailConfirmOpen && (
        <EmailConfirmation
          open={emailConfirmOpen}
          onClose={() => setEmailConfirmOpen(false)}
          user={user}
        />
      )}

      {phoneConfirmOpen && (
        <PhoneConfirmation
          open={phoneConfirmOpen}
          onClose={() => setPhoneConfirmOpen(false)}
          user={user}
        />
      )}

      <StaffDataGrid
        isRefetching={isRefetching}
        onSetIsRefetching={(value) => {
          setIsRefetching(value);
        }}
        modalHandler={(type, payload) => modalOpenHandler(type, payload)}
      />
    </>
  );
};

export default StaffManager;
