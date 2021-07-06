import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'

const ListItem = ({ onChange, onDelete, value, value1, value2 }) => {
    return (
        <div class="mt-6">
            <input
                class="p-3 font-bold bg-gray-100 text-gray-600 border border-gray-300 hidden lg:table-cell"
                value={value}
                readonly="readonly"
            />
            <input
                class="p-3 font-bold bg-gray-100 text-gray-600 border border-gray-300 hidden lg:table-cell"
                value={value1}
                readonly="readonly"
            />
            <input
                class="p-3 font-bold bg-gray-100 text-gray-600 border border-gray-300 hidden lg:table-cell"
                value={value2}
                readonly="readonly"
            />
            <button class="font-semibold py-2 px-3 rounded" onClick={onDelete}> <FontAwesomeIcon icon="trash-alt" /> </button>
        </div>
    );
};

export default ListItem;