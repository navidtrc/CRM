import { useMemo, useState, useEffect } from "react";
import {
  MaterialReactTable,
  useMaterialReactTable,
} from "material-react-table";
import { Button, IconButton, Tooltip } from "@mui/material";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { MRT_Localization_FA } from "material-react-table/locales/fa";
import {
  Edit as EditIcon,
  Key as KeyIcon,
  Delete as DeleteIcon,
  Refresh as RefreshIcon,
  Lock as LockIcon,
  LockOpen as LockOpenIcon,
  Email as EmailIcon,
  MobileOff as MobileOffIcon,
  MobileFriendly as MobileFriendlyIcon,
} from "@mui/icons-material/";
import Swal from "sweetalert2";

const TableGrid = ({ isRefetching, onSetIsRefetching, onOpenModal }) => {
  const [data, setData] = useState([]);
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [rowCount, setRowCount] = useState(0);

  const [columnFilters, setColumnFilters] = useState([]);
  const [globalFilter, setGlobalFilter] = useState("");
  const [sorting, setSorting] = useState([]);
  const [validationErrors, setValidationErrors] = useState({});
  const [pagination, setPagination] = useState({
    pageIndex: 0,
    pageSize: 10,
  });

  useEffect(() => {
    const fetchData = async () => {
      if (!data.length) {
        setIsLoading(true);
      } else {
        onSetIsRefetching(true);
      }

      const fetchURL = new URL(
        "/api/people/get",
        process.env.NODE_ENV === "production"
          ? "https://www.material-react-table.com"
          : "https://localhost:5001"
      );

      fetchURL.searchParams.set(
        "start",
        `${pagination.pageIndex * pagination.pageSize}`
      );
      fetchURL.searchParams.set("size", `${pagination.pageSize}`);
      fetchURL.searchParams.set("filters", JSON.stringify(columnFilters ?? []));
      fetchURL.searchParams.set("globalFilter", globalFilter ?? "");
      fetchURL.searchParams.set("sorting", JSON.stringify(sorting ?? []));

      var myHeaders = new Headers();
      myHeaders.append("Content-Type", "application/json");
      var raw = JSON.stringify({
        type: "staff",
        start: Number(fetchURL.searchParams.get("start")),
        size: Number(fetchURL.searchParams.get("size")),
        globalFilter: fetchURL.searchParams.get("globalFilter"),
        filters: JSON.parse(fetchURL.searchParams.get("filters")),
        sorting: JSON.parse(fetchURL.searchParams.get("sorting"))[0],
      });
      var requestOptions = {
        method: "POST",
        headers: myHeaders,
        body: raw,
        redirect: "follow",
      };

      try {
        const response = await fetch("/api/people/get", requestOptions);
        const json = await response.json();
        const result = json.Data.Data.Data.map((item) => {
          return {
            id: item.Person.Id,
            userId: item.Person.User.Id,
            username: item.Person.User.UserName,
            firstName: item.Person.FirstName,
            lastName: item.Person.LastName,
            email: item.Person.User.Email,
            phoneNumber: item.Person.User.PhoneNumber,
            lockoutEnabled: item.Person.User.LockoutEnabled,
            emailConfirmed: item.Person.User.EmailConfirmed,
            phoneConfirmed: item.Person.User.PhoneNumberConfirmed,
          };
        });

        setData(result);
        setRowCount(json.Data.Data.Total);
      } catch (error) {
        setIsError(true);
        console.error(error);
        return;
      }
      setIsError(false);
      setIsLoading(false);
      onSetIsRefetching(false);
    };
    fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [
    isRefetching,
    columnFilters,
    globalFilter,
    pagination.pageIndex,
    pagination.pageSize,
    sorting,
  ]);

  const columns = useMemo(
    () => [
      {
        accessorKey: "username",
        header: "نام کاربری",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.username,
          helperText: validationErrors?.username,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              username: undefined,
            }),
          //optionally add validation checking for onBlur or onChange
        },
      },
      {
        accessorKey: "firstName",
        header: "نام",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.firstName,
          helperText: validationErrors?.firstName,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              firstName: undefined,
            }),
          //optionally add validation checking for onBlur or onChange
        },
      },
      {
        accessorKey: "lastName",
        header: "نام خانوادگی",
        muiEditTextFieldProps: {
          type: "text",
          required: true,
          error: !!validationErrors?.lastName,
          helperText: validationErrors?.lastName,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              lastName: undefined,
            }),
        },
      },
      {
        accessorKey: "email",
        header: "ایمیل",
        muiEditTextFieldProps: {
          type: "email",
          required: true,
          error: !!validationErrors?.email,
          helperText: validationErrors?.email,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              email: undefined,
            }),
        },
      },
      {
        accessorKey: "phoneNumber",
        header: "شماره تماس",
        muiEditTextFieldProps: {
          type: "phone",
          required: true,
          error: !!validationErrors?.phoneNumber,
          helperText: validationErrors?.phoneNumber,
          //remove any previous validation errors when user focuses on the input
          onFocus: () =>
            setValidationErrors({
              ...validationErrors,
              phoneNumber: undefined,
            }),
        },
      },
    ],
    [validationErrors]
  );

  const handleDeactiveUser = (user) => {
    debugger;
    Swal.fire({
      title: `کاربر ${user.username}؟`,
      text: `وضعیت این کاربر به صورت ${
        user.lockoutEnabled !== true ? "فعال" : "غیرفعال"
      } میباشد.`,
      showCancelButton: true,
      confirmButtonText: user.lockoutEnabled !== true ? "غیر فعال" : "فعال",
      cancelButtonText: "انصراف",
    }).then((result) => {
      if (result.isConfirmed) {
        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
        const raw = {
          UserId: user.userId,
          LockoutEnabled: !user.lockoutEnabled,
        };
        const requestOptions = {
          method: "POST",
          headers: myHeaders,
          body: JSON.stringify(raw),
          redirect: "follow",
        };

        debugger;
        fetch("/api/account/lockout", requestOptions).then(() => {
          Swal.fire("انجام شد", "", "success");
          onSetIsRefetching(true);
        });
      }
    });
  };

  const handleDeleteUser = (user) => {
    debugger;
    Swal.fire({
      title: `آیا از حذف کاربر ${user.username} اظمینان دارید؟`,
      text: "در صورت حذف امکان بازگشت وجود ندارد",
      icon: "warning",
      showCancelButton: true,
      confirmButtonColor: "#3085d6",
      cancelButtonColor: "#d33",
      confirmButtonText: "بله",
      cancelButtonText: "خیر",
    }).then((result) => {
      if (result.isConfirmed) {
        var requestOptions = {
          method: "DELETE",
          redirect: "follow",
        };

        fetch(`/api/people/delete?id=${user.id}`, requestOptions)
          .then(() => {
            Swal.fire({
              title: `کاربر ${user.username} حذف شد`,
              icon: "success",
            });
            onSetIsRefetching(true);
          })
          .catch((error) => console.log("error", error));
      }
    });
  };

  const table = useMaterialReactTable({
    columns,
    data,
    manualFiltering: true,
    manualPagination: true,
    manualSorting: true,
    enableFullScreenToggle: false,
    localization: MRT_Localization_FA,
    createDisplayMode: "",
    enableEditing: true,
    positionActionsColumn: "last",
    onColumnFiltersChange: setColumnFilters,
    onGlobalFilterChange: setGlobalFilter,
    onPaginationChange: setPagination,
    onSortingChange: setSorting,
    rowCount,
    state: {
      columnFilters,
      globalFilter,
      isLoading,
      pagination,
      showAlertBanner: isError,
      showProgressBars: isRefetching,
      sorting,
    },
    displayColumnDefOptions: {
      "mrt-row-actions": {
        size: 150,
        Cell: ({ row }) => (
          <>
            <Button onClick={() => onOpenModal("addedit", row.original)}>
              <EditIcon />
            </Button>
            <Button
              color="warning"
              onClick={() => onOpenModal("changepassword", row.original)}
            >
              <KeyIcon />
            </Button>
            <Button
              color={row.original.lockoutEnabled ? "error" : "success"}
              onClick={() => handleDeactiveUser(row.original)}
            >
              {row.original.lockoutEnabled ? <LockIcon /> : <LockOpenIcon />}
            </Button>
            <Button
              color={row.original.emailConfirmed ? "success" : "error"}
              onClick={() => {
                if (row.original.emailConfirmed) {
                  Swal.fire({
                    icon: "info",
                    text: "ایمیل قبلا تایید شده است. در صورت تغییر آن به تایید مجدد نیاز میباشد.",
                  });
                  return;
                }
                onOpenModal("emailconfirm", row.original);
              }}
            >
              <EmailIcon />
            </Button>
            <Button
              color={row.original.phoneConfirmed ? "success" : "error"}
              onClick={() => {
                if (row.original.phoneConfirmed) {
                  Swal.fire({
                    icon: "info",
                    text: "شماره تماس قبلا تایید شده است. در صورت تغییر آن به تایید مجدد نیاز میباشد.",
                  });
                  return;
                }
                onOpenModal("phoneconfirm", row.original);
              }}
            >
              {row.original.phoneConfirmed ? (
                <MobileFriendlyIcon />
              ) : (
                <MobileOffIcon />
              )}
            </Button>
            <Button
              color="error"
              onClick={() => handleDeleteUser(row.original)}
            >
              <DeleteIcon />
            </Button>
          </>
        ),
      },
    },
    renderTopToolbarCustomActions: () => (
      <>
        <Button variant="contained" onClick={() => onOpenModal("addedit")}>
          کاربر جدید
        </Button>
        <Tooltip arrow title="Refresh Data">
          <IconButton onClick={() => onSetIsRefetching(true)}>
            <RefreshIcon />
          </IconButton>
        </Tooltip>
      </>
    ),
  });

  return <MaterialReactTable table={table} />;
};

const queryClient = new QueryClient();

const CustomerDataGrid = ({ modalHandler, isRefetching, onSetIsRefetching }) => (
  <QueryClientProvider client={queryClient}>
    <TableGrid
      onOpenModal={(type, payload) => modalHandler(type, payload)}
      onSetIsRefetching={onSetIsRefetching}
      isRefetching={isRefetching}
    />
  </QueryClientProvider>
);

export default CustomerDataGrid;
