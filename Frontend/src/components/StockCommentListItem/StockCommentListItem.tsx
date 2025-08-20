
import { FaUserCircle } from "react-icons/fa";
import type { CommentGet } from "../../Models/Comment";

type Props = {
  comment: CommentGet;
};

const StockCommentListItem = ({ comment }: Props) => {
  return (
    <div className="relative flex flex-col gap-3 p-5 mb-5 border border-gray-200 rounded-2xl bg-white shadow-md hover:shadow-lg transition-shadow duration-200">
      {/* Header: Title + User */}
      <div className="flex items-center justify-between">
        <h3 className="text-lg font-medium text-gray-900">{comment.title}</h3>
        <div className="flex items-center gap-1 text-sm text-gray-500">
          <FaUserCircle className="text-lightGreen" />
          {comment.createdBy}
        </div>
      </div>

      {/* Content */}
      <p className="text-gray-600 leading-relaxed">{comment.content}</p>
    </div>
  );
};

export default StockCommentListItem;