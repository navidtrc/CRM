import * as React from "react";

export const InquiryCallActionCustomCell = (props) => {
  const { dataItem } = props;
  return (
    <td className="k-command-cell">
      <button
        className="btn btn-warning btn-sm ml-1"
        onClick={() => props.onEditClick(dataItem)}
      >
        ویرایش
      </button>
      <button
        className="btn btn-danger btn-sm ml-1"
        onClick={() => props.onDeleteClick(dataItem)}
      >
        حذف
      </button>
    </td>
  );
};
